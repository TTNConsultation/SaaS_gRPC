using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dal.Sp
{
  public interface ICollectionSpProperty
  {
    ISpProperty Get(string typename, OperationType op);

    ISpProperty Get<T>(OperationType op) => Get(typeof(T).Name, op);
  }

  public interface ISpProperty
  {
    SqlCommand SqlCommand(string conStr);

    IParameter Parameter(string name);

    bool IsEqual(string spName, OperationType op);
  }

  public interface IParameter
  {
    string ParameterName { get; }

    int StoreProcedureId { get; }

    SqlParameter SqlParameter(object value);
  }

  public sealed class CollectionSpProperty : ICollectionSpProperty
  {
    private readonly IEnumerable<ISpProperty> SpProperties;

    public CollectionSpProperty(ICollectionMapper mappers, IConnectionManager connectionManager)
    {
      SpProperties = Read(mappers, connectionManager.App());
    }

    public ISpProperty Get(string typename, OperationType op) => SpProperties.FirstOrDefault(sp => sp.IsEqual(typename, op));

    private IEnumerable<ISpProperty> Read(ICollectionMapper mappers, string conStr)
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

      var ret = new HashSet<ISpProperty>();
      var map = mappers.Get<SpProperty>();

      while (reader.Read())
      {
        var prop = map.Parse<SpProperty>(reader);
        prop.SetParameters(parameters.Where(p => p.StoreProcedureId == prop.Id));
        ret.Add(prop);
      }

      return ret;
    }

    private IEnumerable<IParameter> ReadSpParameter(IMapper map, string conStr)
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

  public partial class SpProperty : ISpProperty
  {
    private IEnumerable<IParameter> Parameters;

    internal void SetParameters(IEnumerable<IParameter> pars)
    {
      Parameters = pars ?? new HashSet<IParameter>();
    }

    public SqlCommand SqlCommand(string conStr) =>
      new SqlCommand(FullName, new SqlConnection(conStr))
      {
        CommandType = CommandType.StoredProcedure,
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

      return size <= Precision ? size : -1;
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