using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dal.Sp
{
  public interface ICollectionSpInfo : IObject
  {
    ISpInfo Get(string typename, OperationType op);

    ISpInfo Get<T>(OperationType op) => Get(typeof(T).Name, op);
  }

  public interface ISpInfo : IObject
  {
    SqlCommand SqlCommand(string conStr);

    IParameter Parameter(string name);
  }

  public interface IParameter : IObject
  {
    bool IsName(string name);

    SqlParameter SqlParameter(object value);
  }

  public sealed class CollectionSpInfo : ICollectionSpInfo
  {
    private readonly IEnumerable<SpInfo> SpInfos;

    public CollectionSpInfo(ICollectionMapper mappers, IConnectionManager connectionManager)
    {
      SpInfos = Read(mappers, connectionManager.App());
    }

    public ISpInfo Get(string typename, OperationType op) => SpInfos.FirstOrDefault(sp => sp.Type.IsEqual(typename) && sp.Op.IsEqual(op.ToString()));

    public bool IsNotNull() => SpInfos.Any();

    private IEnumerable<SpInfo> Read(ICollectionMapper mappers, string conStr)
    {
      var parameters = ReadSpParameter(mappers, conStr);

      string spName = typeof(SpProperty).SpName(Constant.APP, nameof(OperationType.R));

      using var sqlcmd = new SqlCommand(spName, new SqlConnection(conStr))
      {
        CommandType = CommandType.StoredProcedure
      };

      sqlcmd.Connection.Open();
      using var reader = sqlcmd.ExecuteReader();

      var ret = new HashSet<SpInfo>();
      var map = mappers.Get<SpProperty>();

      while (reader.Read())
      {
        var prop = map.Parse<SpProperty>(reader);
        var pars = parameters.Where(p => p.SpId == prop.Id);

        ret.Add(new SpInfo(prop, pars));
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

  public sealed class SpInfo : ISpInfo
  {
    private readonly SpProperty Property;
    private readonly IEnumerable<IParameter> Parameters;

    internal string Op => Property.Op;
    internal string Type => Property.Type;

    public SqlCommand SqlCommand(string conStr) =>
      new SqlCommand(Property.FullName, new SqlConnection(conStr))
      {
        CommandType = CommandType.StoredProcedure,
      };

    public IParameter Parameter(string name) => Parameters.FirstOrDefault(p => p.IsName(name.AsParameter()));

    public bool IsNotNull() => Property.Id > 0;

    internal SpInfo(SpProperty prop, IEnumerable<SpParameter> pars)
    {
      Property = prop;
      Parameters = pars ?? new HashSet<SpParameter>();
    }
  }

  internal sealed class SpProperty
  {
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Schema { get; set; }
    public string Type { get; set; }
    public string Op { get; set; }
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

    public bool IsName(string name)
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

    public bool IsNotNull() => SpId > 0;
  }
}