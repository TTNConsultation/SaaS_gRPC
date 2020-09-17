using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Dal.Sp;
using Microsoft.Data.SqlClient;

namespace Dal.SpProperty
{
  public sealed class CollectionSpProperty : ICollectionSpProperty
  {
    private readonly IEnumerable<IStoreProcedure> SpProperties;

    public CollectionSpProperty(ICollectionMapper mappers, IConnectionManager connectionManager)
    {
      SpProperties = Read(mappers, connectionManager.App());
    }

    public IStoreProcedure Get(string typename, OperationType op) => SpProperties.FirstOrDefault(sp => sp.IsEqual(typename, op));

    private IEnumerable<IStoreProcedure> Read(ICollectionMapper mappers, string conStr)
    {
      var parameters = ReadSpParameter(mappers.Get<SpParameter>(), conStr);

      string spName = typeof(SpProperty).SpName(Constant.APP, nameof(OperationType.R));

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

    private IEnumerable<IStoreProcedureParameter> ReadSpParameter(IMapper map, string conStr)
    {
      string spName = typeof(SpParameter).SpName(Constant.APP, nameof(OperationType.R));
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

  public partial class SpProperty : IStoreProcedure
  {
    internal IEnumerable<IStoreProcedureParameter> Parameters { private get; set; }

    public SqlCommand SqlCommand(string conStr) =>
      new SqlCommand(FullName, new SqlConnection(conStr))
      {
        CommandType = CommandType.StoredProcedure
      };

    public IStoreProcedureParameter Parameter(string name) => Parameters.FirstOrDefault(p => p.ParameterName.IsEqual(name.AsParameter()));

    public bool IsEqual(string spName, OperationType op) => Type.IsEqual(spName) && Op.IsEqual(op.ToString());
  }

  public partial class SpParameter : IStoreProcedureParameter
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