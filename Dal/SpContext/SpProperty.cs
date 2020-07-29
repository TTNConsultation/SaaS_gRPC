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

    bool IsEqual(string typename, OperationType op);
  }

  public interface IParameter
  {
    bool IsEqual(string name);

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
      var parameters = ReadSpParameter(mappers, conStr);

      string spName = typeof(SpProperty).SpName(Constant.APP, nameof(OperationType.R));

      using var sqlcmd = new SqlCommand(spName, new SqlConnection(conStr))
      {
        CommandType = CommandType.StoredProcedure
      };

      sqlcmd.Connection.Open();
      using var reader = sqlcmd.ExecuteReader();

      var ret = new HashSet<ISpProperty>();
      var map = mappers.Get<SpProperty>();

      while (reader.Read())
      {
        var prop = map.Parse<SpProperty>(reader);
        prop.SetParameters(parameters.Where(p => p.SpId == prop.Id));
        ret.Add(prop);
      }

      return ret;
    }

    private IEnumerable<SpParameter> ReadSpParameter(ICollectionMapper mappers, string conStr)
    {
      string spName = typeof(SpParameter).SpName(Constant.APP, nameof(OperationType.R));
      using var sqlcmd = new SqlCommand(spName, new SqlConnection(conStr))
      {
        CommandType = CommandType.StoredProcedure
      };
      sqlcmd.Connection.Open();

      using var reader = sqlcmd.ExecuteReader();
      return reader.Parse<SpParameter>(mappers.Get<SpParameter>());
    }
  }

  internal sealed class SpProperty : ISpProperty
  {
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Schema { get; set; }
    public string Type { get; set; }
    public string Op { get; set; }

    private IEnumerable<IParameter> Parameters = new HashSet<IParameter>();

    public void SetParameters(IEnumerable<IParameter> pars)
    {
      Parameters = pars ?? new HashSet<IParameter>();
    }

    public SqlCommand SqlCommand(string conStr) =>
      new SqlCommand(FullName, new SqlConnection(conStr))
      {
        CommandType = CommandType.StoredProcedure,
      };

    public IParameter Parameter(string name) => Parameters.FirstOrDefault(p => p.IsEqual(name.AsParameter()));

    public bool IsEqual(string typename, OperationType op) => Type.IsEqual(typename) && Op.IsEqual(op.ToString());
  }

  internal sealed class SpParameter : IParameter
  {
    public string SpName { get; set; }
    public int SpId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int MaxLength { get; set; }
    public int Precision { get; set; }
    public int Scale { get; set; }
    public int Order { get; set; }
    public bool IsOutput { get; set; }
    public string Collation { get; set; }

    private int Size(object value)
    {
      if (value == null || value == DBNull.Value || string.IsNullOrEmpty(Collation))
        return MaxLength;

      var size = value.ToString().Length;

      return size <= Precision ? size : -1;
    }

    public bool IsEqual(string name)
    {
      return Name.IsEqual(name);
    }

    public SqlParameter SqlParameter(object value) =>
      new SqlParameter(Name, Type.ToSqlDbType())
      {
        Direction = IsOutput ? ParameterDirection.Output : ParameterDirection.Input,
        Value = value ?? DBNull.Value,
        Size = Size(value)
      };
  }
}