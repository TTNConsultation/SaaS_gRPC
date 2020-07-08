using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.Data.SqlClient;

namespace Dal.Sp
{
  public sealed class SpMappers : ISpMappers
  {
    private readonly HashSet<ISpMapper> mappers = new HashSet<ISpMapper>();

    public ISpMapper FirstOrDefault(string typename) => mappers.FirstOrDefault(sp => sp.IsType(typename));

    public T Add<T>(SqlDataReader reader, out ISpMapper map) where T : new()
    {
      map = new SpMapper(typeof(T).Name);
      mappers.Add(map);

      return map.Build<T>(reader);
    }
  }

  public sealed class SpMapper : ISpMapper
  {
    private readonly string Name;
    private IDictionary<int, PropertyInfo> Map;

    internal SpMapper(string name)
    {
      Name = name;
    }

    public T Build<T>(SqlDataReader reader) where T : new()
    {
      Map = new Dictionary<int, PropertyInfo>();
      var propInfos = typeof(T).GetProperties();
      var ret = new T();

      for (int i = 0; i < reader.FieldCount; i++)
      {
        var propInfo = propInfos?.FirstOrDefault(pi => pi.Name.IsEqual(reader.GetName(i)));
        if (propInfo != null)
        {
          propInfo.SetValue(ret, Convert.ChangeType(reader[i], propInfo.PropertyType));
          Map.Add(new KeyValuePair<int, PropertyInfo>(i, propInfo));
        }
      }

      return ret;
    }

    public T Parse<T>(SqlDataReader reader) where T : new()
    {
      var ret = new T();
      foreach (var m in Map)
      {
        m.Value.SetValue(ret, Convert.ChangeType(reader[m.Key], m.Value.PropertyType));
      }
      return ret;
    }

    public bool IsType(string typename)
    {
      return Name.IsEqual(typename);
    }
  }
}