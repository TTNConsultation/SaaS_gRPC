using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.Data.SqlClient;

namespace Dal.Sp
{
  public interface ICollectionMapper
  {
    IMapper Get(string typename);

    IMapper Get<T>() => Get(typeof(T).Name);
  }

  public interface IMapper
  {
    bool IsType(string typename);

    bool IsType(IMapper map);

    T Parse<T>(SqlDataReader reader) where T : new();
  }

  public sealed class CollectionMapper : ICollectionMapper
  {
    private readonly HashSet<IMapper> mappers = new HashSet<IMapper>();

    private IMapper Add(IMapper map) => mappers.Add(map) ? map : mappers.First(m => m.IsType(map));

    public IMapper Get(string typename) => mappers.FirstOrDefault(m => m.IsType(typename)) ?? Add(new Mapper(typename));
  }

  public sealed class Mapper : IMapper
  {
    private IDictionary<int, PropertyInfo> ReflectionMap;

    private readonly string TypeName;

    internal Mapper(string typename)
    {
      TypeName = typename;
    }

    private T BuildMap<T>(SqlDataReader reader) where T : new()
    {
      ReflectionMap = new Dictionary<int, PropertyInfo>();
      var propInfos = typeof(T).GetProperties();
      var ret = new T();

      for (int i = 0; i < reader.FieldCount; i++)
      {
        var propInfo = propInfos?.FirstOrDefault(pi => pi.Name.IsEqual(reader.GetName(i)));
        if (propInfo != null)
        {
          propInfo.SetValue(ret, Convert.ChangeType(reader[i], propInfo.PropertyType));
          ReflectionMap.Add(i, propInfo);
        }
      }

      return ret;
    }

    private T UseMap<T>(SqlDataReader reader) where T : new()
    {
      var ret = new T();
      foreach (var kpv in ReflectionMap)
      {
        kpv.Value.SetValue(ret, Convert.ChangeType(reader[kpv.Key], kpv.Value.PropertyType));
      }
      return ret;
    }

    public T Parse<T>(SqlDataReader reader) where T : new() =>
      (ReflectionMap == null) ? BuildMap<T>(reader) : UseMap<T>(reader);

    public bool IsType(string typename) => TypeName.IsEqual(typename);

    public bool IsType(IMapper map) => map.IsType(this.TypeName);
  }
}