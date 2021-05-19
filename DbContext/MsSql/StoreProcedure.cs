using Utility;
using DbContext.Interfaces;
using DbContext.MsSql.Command;
using Google.Protobuf;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using System.Data;
using System.Linq;
using System.Security.Claims;

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
      _procedures = new CollectionProcedure(_mappers, _connections.App());
    }

    public IReader<T> GetAppReader<T>(OperationType op) where T : IMessage<T>, new() =>
      new Reader<T>(_mappers.Get<T>(), _procedures.Get<T>(op), _connections.App());

    public IReader<T> GetReader<T>(ClaimsPrincipal uc, OperationType op) where T : IMessage<T>, new() =>
      new Reader<T>(_mappers.Get<T>(), _procedures.Get<T>(op), _connections.Get(uc));

    public IWriter<T> GetWriter<T>(ClaimsPrincipal uc, OperationType op) where T : IMessage<T>, new() =>
      new Writer<T>(_mappers.Get<T>(), _procedures.Get<T>(op), _procedures.Get<T>(OperationType.R), _connections.Get(uc));
  }

  internal sealed class ConnectionManager
  {
    private readonly IDictionary<string, string> _connectionStrings;

    public ConnectionManager(IConfiguration config)
    {
      _connectionStrings = config.GetSection(STR.CONNECTIONSTRINGS)
                                ?.GetChildren()
                                ?.ToDictionary(s => s.Key, s => s.Value) ?? new Dictionary<string, string>();
    }

    private string Get(string schema) => _connectionStrings[schema];

    public string App() => Get(STR.APP);

    public string Get(ClaimsPrincipal cp) =>
      Get(cp?.FindFirst(ClaimTypes.Role)?.Value ?? STR.APP);
  }

  internal sealed class CollectionProcedure
  {
    private readonly ICollection<Procedure> _procedures;

    public CollectionProcedure(CollectionMapper maps, string connectionString) =>
      _procedures = ReadProcedure(maps, connectionString);

    public Procedure Get<T>(OperationType op) => _procedures.FirstOrDefault(p => p.IsEqual(typeof(T).Name, op.ToString()));

    private static ICollection<Procedure> ReadProcedure(CollectionMapper maps, string conStr)
    {
      var parameters = ReadParameter(maps.Get<Parameter>(), conStr);

      var ret = new HashSet<Procedure>();
      var map = maps.Get<Procedure>();
      string spName = STR.APP.DotAnd(nameof(Procedure)).UnderscoreAnd(nameof(OperationType.R));

      using var sqlCon = new SqlConnection(conStr);
      var sqlcmd = new SqlCommand(spName, sqlCon) { CommandType = CommandType.StoredProcedure };
      sqlCon.Open();

      using var reader = sqlcmd.ExecuteReader();
      while (reader.Read())
      {
        var p = map.Parse<Procedure>(reader);
        p.Parameters.AddRange(parameters.Where(par => par.SpId == p.Id));
        ret.Add(p);
      }

      return ret;
    }

    private static ICollection<Parameter> ReadParameter(Mapper map, string conStr)
    {
      var ret = new HashSet<Parameter>();
      string spName = STR.APP.DotAnd(nameof(Parameter)).UnderscoreAnd(nameof(OperationType.R));

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
}