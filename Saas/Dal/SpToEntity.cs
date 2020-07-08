using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

using Microsoft.Data.SqlClient;

namespace Saas.Dal
{
  internal class CollectionSpToEntity : ICollectionSpToEntity
  {
    private readonly HashSet<ISpToEntity> mappers = new HashSet<ISpToEntity>();

    public ISpToEntity FirstOrDefault(string typename) => mappers.FirstOrDefault(sp => sp.Type(typename));

    public T Add<T>(SqlDataReader reader, out ISpToEntity map) where T : new()
    {
      map = new SpToEntity(Constant.TypeName<T>());
      mappers.Add(map);

      return map.Build<T>(reader);
    }
  }

  internal sealed class SpToEntity : ISpToEntity
  {
    private readonly string Name;
    private IDictionary<int, PropertyInfo> Map;

    internal SpToEntity(string name)
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

    public bool Type(string typename)
    {
      return Name.IsEqual(typename);
    }
  }
}