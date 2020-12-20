using System;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Microsoft.Data.SqlClient;

using StoreProcedure.Interface;

namespace StoreProcedure
{
  public sealed class CollectionMapper : ICollectionMapper
  {
    private readonly HashSet<IMapper> _mappers = new HashSet<IMapper>();

    public IMapper Get(Type type) => _mappers.FirstOrDefault(m => m.IsType(type)) ?? _mappers.Append(new Mapper(type)).Last();
  }

  internal sealed class Mapper : IMapper
  {
    private readonly Type _type;
    private IDictionary<int, int> _fieldMap;

    public Mapper(Type type)
    {
      _type = type;
    }

    private T BuildMap<T>(SqlDataReader reader) where T : IMessage, new()
    {
      _fieldMap = new Dictionary<int, int>();
      var objT = new T();
      FieldDescriptor fd;

      for (int i = 0; i < reader.FieldCount; i++)
      {
        fd = objT.Descriptor.FindFieldByName(reader.GetName(i));
        if (fd != null)
        {
          fd.Accessor.SetValue(objT, fd.ChangeType(reader[i]));
          _fieldMap.Add(i, fd.FieldNumber);
        }
      }

      return objT;
    }

    private T UseMap<T>(SqlDataReader reader) where T : IMessage, new()
    {
      var objT = new T();
      FieldDescriptor fd;

      foreach (var fm in _fieldMap)
      {
        fd = objT.Descriptor.Fields[fm.Value];
        fd.Accessor.SetValue(objT, fd.ChangeType(reader[fm.Key]));
      }

      return objT;
    }

    public bool IsType(Type type) => _type == type;

    public T Parse<T>(SqlDataReader reader) where T : IMessage<T>, new() =>
      (_fieldMap == null) ? BuildMap<T>(reader) : UseMap<T>(reader);
  }
}