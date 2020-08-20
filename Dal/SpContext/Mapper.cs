using System.Collections.Generic;
using System.Linq;
using Google.Protobuf;
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
    string TypeName { get; }

    bool IsEqual(IMapper map) => TypeName.IsEqual(map.TypeName);

    T Parse<T>(SqlDataReader reader) where T : IMessage, new();
  }

  public sealed class CollectionMapper : ICollectionMapper
  {
    private readonly HashSet<IMapper> mappers = new HashSet<IMapper>();

    private IMapper Add(IMapper map) => mappers.Add(map) ? map : mappers.First(m => m.IsEqual(map));

    public IMapper Get(string typeName) => mappers.FirstOrDefault(m => m.TypeName.IsEqual(typeName)) ?? Add(new Mapper(typeName));
  }

  public sealed class Mapper : IMapper
  {
    private IDictionary<int, int> FieldMap;

    public string TypeName { get; }

    internal Mapper(string typename)
    {
      TypeName = typename;
    }

    private T MapAndParse<T>(SqlDataReader reader) where T : IMessage, new()
    {
      FieldMap = new Dictionary<int, int>();
      var objT = new T();

      for (int i = 0; i < reader.FieldCount; i++)
      {
        var fd = objT.Descriptor.FindFieldByName(reader.GetName(i));
        if (fd != null)
        {
          fd.Accessor.SetValue(objT, fd.ChangeType(reader[i]));
          FieldMap.Add(i, fd.FieldNumber);
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
        var fd = objT.Descriptor.Fields[fm.Value];
        fd.Accessor.SetValue(objT, fd.ChangeType(reader[fm.Key]));
      }
      return objT;
    }
  }
}