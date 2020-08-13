using System;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Microsoft.Data.SqlClient;

namespace Dal.Sp
{
  public interface ICollectionMapper
  {
    IMapper Get(string typeName);

    IMapper Get<T>() => Get(typeof(T).Name);
  }

  public interface IMapper
  {
    bool IsType(string typename);

    bool IsType(IMapper map);

    T Parse<T>(SqlDataReader reader) where T : IMessage, new();
  }

  public sealed class CollectionMapper : ICollectionMapper
  {
    private readonly HashSet<IMapper> mappers = new HashSet<IMapper>();

    private IMapper Add(IMapper map) => mappers.Add(map) ? map : mappers.First(m => m.IsType(map));

    public IMapper Get(string typeName) => mappers.FirstOrDefault(m => m.IsType(typeName)) ?? Add(new Mapper(typeName));
  }

  public sealed class Mapper : IMapper
  {
    private IDictionary<int, int> ReflectionMap;

    private readonly string TypeName;

    internal Mapper(string typename)
    {
      TypeName = typename;
    }

    private T BuildMap<T>(SqlDataReader reader) where T : IMessage, new()
    {
      ReflectionMap = new Dictionary<int, int>();
      var objT = new T();

      for (int i = 0; i < reader.FieldCount; i++)
      {
        var fd = objT.Descriptor.FindFieldByName(reader.GetName(i));
        if (fd != null)
        {
          fd.Accessor.SetValue(objT, fd.ChangeType(reader[i]));
          ReflectionMap.Add(i, fd.FieldNumber);
        }
      }

      return objT;
    }

    private T UseMap<T>(SqlDataReader reader) where T : IMessage, new()
    {
      var objT = new T();
      FieldDescriptor fd;
      foreach (var m in ReflectionMap)
      {
        fd = objT.Descriptor.Fields[m.Value];
        fd.Accessor.SetValue(objT, fd.ChangeType(reader[m.Key]));
      }
      return objT;
    }

    public T Parse<T>(SqlDataReader reader) where T : IMessage, new()
    {
      if (!typeof(T).Name.IsEqual(TypeName))
        throw new Exception();

      return (ReflectionMap == null) ? BuildMap<T>(reader) : UseMap<T>(reader);
    }

    public bool IsType(string typename) => TypeName.IsEqual(typename);

    public bool IsType(IMapper map) => map.IsType(this.TypeName);
  }
}