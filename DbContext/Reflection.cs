using Google.Protobuf;
using Google.Protobuf.Reflection;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DbContext
{
  internal sealed class CollectionMapper
  {
    private readonly ICollection<Mapper> _mappers = new HashSet<Mapper>();

    private Mapper Get(Type type) => _mappers.FirstOrDefault(m => m.Equals(type)) ?? _mappers.Append(new Mapper(type)).Last();

    public Mapper Get<T>() where T : IMessage<T> => Get(typeof(T));
  }

  internal sealed class Mapper
  {
    private readonly Type _type;
    private IDictionary<int, int> _fieldMap = new Dictionary<int, int>();

    public Mapper(Type type)
    {
      _type = type;
    }

    private T Build<T>(SqlDataReader reader) where T : IMessage<T>, new()
    {
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

    private T Map<T>(SqlDataReader reader) where T : IMessage<T>, new()
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

    public bool Equals(Type type) => _type == type;

    public T Parse<T>(SqlDataReader reader) where T : IMessage<T>, new() =>
      (_fieldMap.Count == 0) ? Build<T>(reader) : Map<T>(reader);
  }
}