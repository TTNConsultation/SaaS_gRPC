using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using StoreProcedure;
using StoreProcedure.Interface;

namespace Dal
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

    public string App() => Get(Constant.APP);
  }

  internal sealed class CollectionSpProperty : ICollectionProcedure
  {
    private readonly ICollection<IProcedure> _storeProcedures;

    public CollectionSpProperty(ICollectionMapper maps, IConnectionManager conManager)
    {
      _storeProcedures = Initialize(maps, conManager.App());
    }

    public IProcedure Get(string typename, OperationType op) => _storeProcedures.FirstOrDefault(sp => sp.IsEqual(typename, op));

    public ICollection<IProcedure> Initialize(ICollectionMapper maps, string conStr)
    {
      var parameters = GetParameters(maps.Get<SpParameter>(), conStr);
      var ret = new HashSet<IProcedure>();
      var map = maps.Get<SpProperty>();

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

    private static IEnumerable<IParameter> GetParameters(IMapper map, string conStr)
    {
      string spName = Constant.APP.DotAnd(nameof(SpParameter)).UnderscoreAnd(nameof(OperationType.R));

      using var sqlcon = new SqlConnection(conStr);
      var sqlcmd = new SqlCommand(spName, sqlcon)
      {
        CommandType = CommandType.StoredProcedure
      };
      sqlcon.Open();

      using var reader = sqlcmd.ExecuteReader();
      return reader.Parse<SpParameter>(map);
    }
  }

  public partial class SpProperty : IProcedure
  {
    internal IEnumerable<IParameter> Parameters { private get; set; }

    public SqlCommand SqlCommand(string conStr) =>
      new SqlCommand(FullName, new SqlConnection(conStr))
      {
        CommandType = CommandType.StoredProcedure
      };

    public IParameter Parameter(string name) => Parameters.FirstOrDefault(p => p.ParameterName.IsEqual(name.AsParameter()));

    public bool IsEqual(string spName, OperationType op) => Type.IsEqual(spName) && Op.IsEqual(op.ToString());
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