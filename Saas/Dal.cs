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

  internal sealed class CollectionProcedure : ICollectionProcedure
  {
    private readonly ICollection<IProcedure> _storeProcedures;

    public CollectionProcedure(ICollectionMapper maps, IConnectionManager connectionManager) =>
      _storeProcedures = Procedure.Read(maps, connectionManager.App());

    public IProcedure Get(string baseName, OperationType op) =>
      _storeProcedures.FirstOrDefault(sp => sp.IsEqual(baseName, op));
  }

  public partial class Procedure : IProcedure
  {
    public IEnumerable<IParameter> Parameters { get; set; }

    public SqlCommand SqlCommand(string conStr) =>
      new SqlCommand(FullName, new SqlConnection(conStr)) { CommandType = CommandType.StoredProcedure };

    public IParameter Parameter(string name) => Parameters.FirstOrDefault(p => p.ParameterName.IsEqual(name.AsParameter()));

    public bool IsEqual(string spName, OperationType op) => this.Type.IsEqual(spName) && Op.IsEqual(op.ToString());

    internal static ICollection<IProcedure> Read(ICollectionMapper maps, string conStr)
    {
      var parameters = Saas.Dal.Parameter.Read(maps.Get<Parameter>(), conStr);

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
        sp.Parameters = parameters.Where(p => p.StoreProcedureId == sp.Id);
        ret.Add(sp);
      }

      return ret;
    }
  }

  public partial class Parameter : IParameter
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

    internal static IEnumerable<IParameter> Read(IMapper map, string conStr)
    {
      string spName = Constant.APP.DotAnd(nameof(Parameter)).UnderscoreAnd(nameof(OperationType.R));

      using var sqlCon = new SqlConnection(conStr);
      var sqlcmd = new SqlCommand(spName, sqlCon) { CommandType = CommandType.StoredProcedure };
      sqlCon.Open();

      using var reader = sqlcmd.ExecuteReader();

      return reader.Parse<Parameter>(map);
    }
  }
}