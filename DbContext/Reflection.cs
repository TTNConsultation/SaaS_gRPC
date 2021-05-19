using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbContext
{
  internal sealed class CollectionMapper
  {
    private readonly ICollection<Mapper> _mappers = new HashSet<Mapper>();

    private Mapper Get(Type type) => _mappers.FirstOrDefault(m => m.Equals(type)) ??
                                     _mappers.Append(new Mapper(type)).Last();

    public Mapper Get<T>() where T : IMessage<T> => Get(typeof(T));
  }

  internal sealed class Mapper
  {
    public readonly Type TypeMap;
    private IDictionary<int, int> _fieldMap = new Dictionary<int, int>();

    public Mapper(Type type)
    {
      TypeMap = type;
    }

    private T Build<T>(IDataReader reader) where T : IMessage<T>, new()
    {
      var objT = new T();

      for (int i = 0; i < reader.FieldCount; i++)
      {
        var fd = objT.Descriptor.FindFieldByName(reader.GetName(i));
        if (fd != null)
        {
          fd.Accessor.SetValue(objT, fd.ChangeType(reader[i]));
          _fieldMap.Add(i, fd.FieldNumber);
        }
      }

      return objT;
    }

    private T Map<T>(IDataReader reader) where T : IMessage<T>, new()
    {
      var objT = new T();

      foreach (var fm in _fieldMap)
      {
        var fd = objT.Descriptor.Fields[fm.Value];
        fd.Accessor.SetValue(objT, fd.ChangeType(reader[fm.Key]));
      }

      return objT;
    }

    public T Parse<T>(IDataReader reader) where T : IMessage<T>, new() =>
      (_fieldMap.Count == 0) ? Build<T>(reader) : Map<T>(reader);

    public override bool Equals(object obj) =>
      (obj is Type) ? ((Type)obj).Equals(TypeMap) : (obj is Mapper) ? ((Mapper)obj).TypeMap.Equals(TypeMap) : false;

    public override int GetHashCode() => TypeMap.GetHashCode();
  }
}