using System.Security.Claims;
using Protos.Shared.Interfaces;
using DbContext.Command;
using Google.Protobuf;
using Protos.Shared;
using System.Collections.Generic;
using Protos.Shared.Dal;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace DbContext
{
  public sealed class StoreProcedure : IDbContext
  {
    private readonly IConnectionManager _connections;
    private readonly ICollectionMapper _mappers;
    private readonly ICollectionProcedure _procedures;

    public StoreProcedure(IConfiguration config)
    {
      _mappers = new CollectionMapper();
      _connections = new ConnectionManager(config);
      _procedures = new CollectionProcedure(_mappers, _connections);
    }

    public IExecuteReader<T> ReferenceData<T>(int rootId) where T : IMessage<T>, new() =>
      new ExecuteReader<T>(new Security(_connections.App(), rootId),
                          _procedures.Get<T>(OperationType.R),
                          _mappers.Get<T>());

    public IExecuteReader<T> Read<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : IMessage<T>, new() =>
      new ExecuteReader<T>(new Security(_connections, uc, appId),
                          _procedures.Get<T>(op),
                          _mappers.Get<T>());

    public IExecuteNonQuery<T> Write<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : IMessage<T>, new() =>
      new ExecuteNonQuery<T>(new Security(_connections, uc, appId),
                            _procedures.Get<T>(op),
                            _procedures.Get<T>(OperationType.R),
                            _mappers.Get<T>());
  }

  internal sealed class ConnectionManager : IConnectionManager
  {
    private readonly IDictionary<string, string> _connectionStrings;

    public ConnectionManager(IConfiguration config)
    {
      _connectionStrings = config.GetSection(Constant.CONNECTIONSTRINGS)
                                ?.GetChildren()
                                ?.ToDictionary(s => s.Key, s => s.Value) ?? new Dictionary<string, string>();
    }

    public string Get(string schema) => _connectionStrings.FirstOrDefault(s => s.Key.IsEqual(schema)).Value;
  }

  internal sealed class CollectionProcedure : ICollectionProcedure
  {
    private readonly ICollection<IProcedure> _storeProcedures;

    public CollectionProcedure(ICollectionMapper maps, IConnectionManager connectionManager) =>
      _storeProcedures = ReadProcedure(maps, connectionManager.App());

    public IProcedure Get(string baseName, OperationType op) =>
      _storeProcedures.FirstOrDefault(sp => sp.IsEqual(baseName, op));

    private static ICollection<IProcedure> ReadProcedure(ICollectionMapper maps, string conStr)
    {
      var parameters = ReadParameter(maps.Get<Parameter>(), conStr);

      var ret = new HashSet<IProcedure>();
      var map = maps.Get<Procedure>();

      string spName = Constant.APP.DotAnd(nameof(Procedure)).UnderscoreAnd(nameof(OperationType.R));

      using var sqlCon = new SqlConnection(conStr);
      var sqlcmd = new SqlCommand(spName, sqlCon) { CommandType = CommandType.StoredProcedure };
      sqlCon.Open();

      using var reader = sqlcmd.ExecuteReader();

      while (reader.Read())
      {
        var sp = map.Parse<Procedure>(reader);
        sp.Parameters = parameters.Where(p => p.ProcedureId == sp.Id);
        ret.Add(sp);
      }

      return ret;
    }

    private static IEnumerable<IParameter> ReadParameter(IMapper map, string conStr)
    {
      string spName = Constant.APP.DotAnd(nameof(Parameter)).UnderscoreAnd(nameof(OperationType.R));

      using var sqlCon = new SqlConnection(conStr);
      var sqlcmd = new SqlCommand(spName, sqlCon) { CommandType = CommandType.StoredProcedure };
      sqlCon.Open();

      using var reader = sqlcmd.ExecuteReader();

      return reader.Parse<Parameter>(map);
    }
  }

  internal sealed class Security
  {
    public readonly int RootId;
    public readonly string ConnectionString;

    public Security(IConnectionManager conmng, ClaimsPrincipal cp, int appid)
    {
      RootId = int.Parse(conmng.Get(Constant.APPID));

      if (cp.IsInRole(Constant.ADMIN))
        ConnectionString = conmng.Get(Constant.ADMIN);
      else if (cp.IsInRole(Constant.USER))
        ConnectionString = conmng.Get(Constant.USER);
      else if (cp.IsInRole(Constant.APP))
        ConnectionString = conmng.Get(Constant.APP);
    }

    internal Security(string conStr, int rootId)
    {
      ConnectionString = conStr;
      RootId = rootId;
    }
  }
}