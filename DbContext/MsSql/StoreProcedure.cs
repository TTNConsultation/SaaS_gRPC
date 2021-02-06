using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Google.Protobuf;

using Constant;
using Protos.Dal;
using DbContext.Interfaces;
using DbContext.MsSql.Command;

namespace DbContext.MsSql
{
  public sealed class StoreProcedure : IDbContext
  {
    private readonly CollectionMapper _mappers = new CollectionMapper();
    private readonly ConnectionManager _connections;
    private readonly CollectionProcedure _procedures;

    public StoreProcedure(IConfiguration config)
    {
      _connections = new ConnectionManager(config);
      _procedures = new CollectionProcedure(_mappers, _connections);
    }

    public IExecuteReader<T> ReferenceData<T>(int rootId) where T : IMessage<T>, new() =>
      new ExecuteReader<T>(new Security(_connections.App(), rootId),
                          _procedures.Get<T>(OperationType.R),
                          _mappers.Get<T>());

    public IExecuteReader<T> GetReader<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : IMessage<T>, new() =>
      new ExecuteReader<T>(new Security(_connections, uc, appId),
                          _procedures.Get<T>(op),
                          _mappers.Get<T>());

    public IExecuteNonQuery<T> GetWriter<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : IMessage<T>, new() =>
      new ExecuteNonQuery<T>(new Security(_connections, uc, appId),
                            _procedures.Get<T>(op),
                            _procedures.Get<T>(OperationType.R),
                            _mappers.Get<T>());
  }

  internal sealed class ConnectionManager
  {
    private readonly IDictionary<string, string> _connectionStrings;

    public ConnectionManager(IConfiguration config)
    {
      _connectionStrings = config.GetSection(StrVal.CONNECTIONSTRINGS)
                                ?.GetChildren()
                                ?.ToDictionary(s => s.Key, s => s.Value) ?? new Dictionary<string, string>();
    }

    public string Get(string schema) => _connectionStrings.ContainsKey(schema) ? _connectionStrings[schema]
                                                                               : string.Empty;

    public string App(string appSchema = StrVal.APP) => Get(appSchema);
  }

  internal sealed class CollectionProcedure
  {
    private readonly ICollection<Procedure> _procedures;

    public CollectionProcedure(CollectionMapper maps, ConnectionManager connectionManager) =>
      _procedures = ReadProcedure(maps, connectionManager.App());

    public Procedure Get(string baseName, OperationType op) =>
      _procedures.FirstOrDefault(sp => sp.IsEqual(baseName, op.ToString()));

    public Procedure Get<T>(OperationType op) where T : IMessage<T> =>
      Get(typeof(T).Name, op);

    private static ICollection<Procedure> ReadProcedure(CollectionMapper maps, string conStr)
    {
      var parameters = ReadParameter(maps.Get<Parameter>(), conStr);

      var ret = new HashSet<Procedure>();
      var map = maps.Get<Procedure>();

      string spName = StrVal.APP.DotAnd(nameof(Procedure)).UnderscoreAnd(nameof(OperationType.R));

      using var sqlCon = new SqlConnection(conStr);
      var sqlcmd = new SqlCommand(spName, sqlCon) { CommandType = CommandType.StoredProcedure };
      sqlCon.Open();

      using var reader = sqlcmd.ExecuteReader();

      while (reader.Read())
      {
        var sp = map.Parse<Procedure>(reader);
        sp.Parameters.AddRange(parameters.Where(p => p.ProcedureId == sp.Id));
        ret.Add(sp);
      }

      return ret;
    }

    private static ICollection<Parameter> ReadParameter(Mapper map, string conStr)
    {
      var ret = new HashSet<Parameter>();
      string spName = StrVal.APP.DotAnd(nameof(Parameter)).UnderscoreAnd(nameof(OperationType.R));

      using var sqlCon = new SqlConnection(conStr);
      var sqlcmd = new SqlCommand(spName, sqlCon) { CommandType = CommandType.StoredProcedure };
      sqlCon.Open();

      using var reader = sqlcmd.ExecuteReader();

      while (reader.Read())
      {
        ret.Add(map.Parse<Parameter>(reader));
      }

      return ret;
    }
  }

  internal sealed class Security
  {
    public readonly int RootId;
    public readonly string ConnectionString;

    public Security(ConnectionManager conmng, ClaimsPrincipal cp, int appid)
    {
      if (!int.TryParse(conmng.Get(StrVal.APP.AndId()), out RootId))
        RootId = appid;

      if (cp.IsInRole(StrVal.ADMIN))
        ConnectionString = conmng.Get(StrVal.ADMIN);
      else if (cp.IsInRole(StrVal.USER))
        ConnectionString = conmng.Get(StrVal.USER);
      else if (cp.IsInRole(StrVal.APP))
        ConnectionString = conmng.Get(StrVal.APP);
    }

    internal Security(string conStr, int rootId)
    {
      ConnectionString = conStr;
      RootId = rootId;
    }
  }
}