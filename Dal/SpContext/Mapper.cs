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
    private IDictionary<int, FieldDescriptor> FieldMap;

    private readonly string TypeName;

    internal Mapper(string typename)
    {
      TypeName = typename;
    }

    private T MapAndParse<T>(SqlDataReader reader) where T : IMessage, new()
    {
      FieldMap = new Dictionary<int, FieldDescriptor>();
      var objT = new T();

      for (int i = 0; i < reader.FieldCount; i++)
      {
        var fd = objT.Descriptor.FindFieldByName(reader.GetName(i));
        if (fd != null)
        {
          fd.Accessor.SetValue(objT, fd.ChangeType(reader[i]));
          FieldMap.Add(i, fd);
        }
      }

      return objT;
    }

    public T Parse<T>(SqlDataReader reader) where T : IMessage, new()
    {
      if (FieldMap == null)
        return MapAndParse<T>(reader);

      var objT = new T();
      foreach (var fm in FieldMap)
      {
        fm.Value.Accessor.SetValue(objT, fm.Value.ChangeType(reader[fm.Key]));
      }
      return objT;
    }

    public bool IsType(string typename) => TypeName.IsEqual(typename);

    public bool IsType(IMapper map) => map.IsType(this.TypeName);
  }
}