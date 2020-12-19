using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using StoreProcedure;
using StoreProcedure.Interface;

namespace Saas.Dal
{
  internal sealed class ConnectionManager : IConnectionManager
  {
    private readonly IDictionary<string, string> _connectionStrings;

    public ConnectionManager(IConfiguration config)
    {
      _connectionStrings = config.GetSection("ConnectionStrings")
                                ?.GetChildren()
                                ?.ToDictionary(s => s.Key, s => s.Value) ?? new Dictionary<string, string>();
    }

    public string Get(string schema) => _connectionStrings.FirstOrDefault(s => s.Key.IsEqual(schema)).Value;
  }

  internal sealed class CollectionStoreProcedure : ICollectionStoreProcedure
  {
    private readonly ICollection<IStoreProcedure> _storeProcedures;

    public CollectionStoreProcedure(ICollectionMapper mappers, IConnectionManager connectionManager)
    {
      _storeProcedures = Initialize(mappers, connectionManager.App());
    }

    public IStoreProcedure Get(string baseName, OperationType op) => _storeProcedures.FirstOrDefault(sp => sp.IsEqual(baseName, op));

    public ICollection<IStoreProcedure> Initialize(ICollectionMapper mappers, string conStr)
    {
      var parameters = GetParameters(mappers, conStr);
      var ret = new HashSet<IStoreProcedure>();
      var map = mappers.Get<SpProperty>();

      string spName = Constant.APP.DotAnd(nameof(SpProperty)).UnderscoreAnd(nameof(OperationType.R));

      using var sqlCon = new SqlConnection(conStr);
      var sqlcmd = new SqlCommand(spName, sqlCon)
      {
        CommandType = CommandType.StoredProcedure
      };
      sqlCon.Open();

      using var reader = sqlcmd.ExecuteReader();

      while (reader.Read())
      {
        var prop = map.Parse<SpProperty>(reader);
        prop.Parameters = parameters.Where(p => p.StoreProcedureId == prop.Id);
        ret.Add(prop);
      }

      return ret;
    }

    private static IEnumerable<IParameter> GetParameters(ICollectionMapper mappers, string conStr)
    {
      string spName = Constant.APP.DotAnd(nameof(SpParameter)).UnderscoreAnd(nameof(OperationType.R));

      using var sqlCon = new SqlConnection(conStr);
      var sqlcmd = new SqlCommand(spName, sqlCon)
      {
        CommandType = CommandType.StoredProcedure
      };
      sqlCon.Open();

      using var reader = sqlcmd.ExecuteReader();

      return reader.Parse<SpParameter>(mappers);
    }
  }

  public partial class SpProperty : IStoreProcedure
  {
    internal IEnumerable<IParameter> Parameters { private get; set; }

    public SqlCommand SqlCommand(string conStr) =>
      new SqlCommand(FullName, new SqlConnection(conStr))
      {
        CommandType = CommandType.StoredProcedure
      };

    public IParameter Parameter(string name) => Parameters.FirstOrDefault(p => p.ParameterName.IsEqual(name.AsParameter()));

    public bool IsEqual(string spName, OperationType op) => this.Type.IsEqual(spName) && Op.IsEqual(op.ToString());
  }

  public partial class SpParameter : IParameter
  {
    private int Size(object value)
    {
      if (value == null || value == DBNull.Value || string.IsNullOrEmpty(Collation))
        return MaxLength;

      var size = value.ToString().Length;

      return size <= MaxLength ? size : -1;
    }

    public string ParameterName => this.Name;

    public int StoreProcedureId => this.SpId;

    public SqlParameter SqlParameter(object value) =>
      new SqlParameter(Name, Type.ToSqlDbType())
      {
        Direction = IsOutput ? ParameterDirection.Output : ParameterDirection.Input,
        Value = value ?? DBNull.Value,
        Size = Size(value)
      };
  }
}