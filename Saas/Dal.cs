using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Microsoft.Data.SqlClient;

using StoreProcedure;
using StoreProcedure.Interface;

namespace Saas.Dal
{
  public sealed class CollectionStoreProcedure : ICollectionStoreProcedure
  {
    private readonly IEnumerable<IStoreProcedure> _storeProcedures;

    public CollectionStoreProcedure(ICollectionMapper mappers, IConnectionManager connectionManager)
    {      
      _storeProcedures = SpProperty_R(mappers, connectionManager.App());
    }

    public IStoreProcedure Get(string baseName, OperationType op) => _storeProcedures.FirstOrDefault(sp => sp.IsEqual(baseName, op));

    private static IEnumerable<IStoreProcedure> SpProperty_R(ICollectionMapper mappers, string conStr)
    {
      var parameters = SpParameter_R(mappers, conStr);

      string spName = Constant.APP.DotAnd(nameof(SpProperty)).UnderscoreAnd(nameof(OperationType.R));

      using var sqlCon = new SqlConnection(conStr);
      var sqlcmd = new SqlCommand(spName, sqlCon)
      {
        CommandType = CommandType.StoredProcedure
      };
      sqlCon.Open();

      using var reader = sqlcmd.ExecuteReader();

      var ret = new HashSet<IStoreProcedure>();
      var map = mappers.Get<SpProperty>();

      while (reader.Read())
      {
        var prop = map.Parse<SpProperty>(reader);
        prop.Parameters = parameters.Where(p => p.StoreProcedureId == prop.Id);
        ret.Add(prop);
      }

      return ret;
    }

    private static IEnumerable<IParameter> SpParameter_R(ICollectionMapper mappers, string conStr)
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