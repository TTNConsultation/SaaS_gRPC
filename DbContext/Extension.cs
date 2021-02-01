using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Google.Protobuf;
using Google.Protobuf.Reflection;
using Microsoft.Data.SqlClient;

using Protos.Shared.Interfaces;

namespace DbContext
{
  internal static class Extension
  {
    public static object ChangeType(this FieldDescriptor fd, object obj)
    {
      return fd.FieldType switch
      {
        FieldType.Double or FieldType.Float => Convert.ChangeType(obj, typeof(float)),
        FieldType.Int64 or FieldType.SFixed64 or FieldType.SInt64 => Convert.ChangeType(obj, typeof(long)),
        FieldType.UInt64 or FieldType.Fixed64 => Convert.ChangeType(obj, typeof(ulong)),
        FieldType.Int32 or FieldType.SFixed32 or FieldType.SInt32 => Convert.ChangeType(obj, typeof(int)),
        FieldType.Fixed32 or FieldType.UInt32 => Convert.ChangeType(obj, typeof(uint)),
        FieldType.Bool => Convert.ChangeType(obj, typeof(bool)),
        FieldType.String => Convert.ChangeType(obj, typeof(string)),
        FieldType.Bytes => Convert.ChangeType(obj, typeof(ByteString)),
        _ => obj,
      };
    }

    public static ICollection<T> Parse<T>(this SqlDataReader reader, IMapper map) where T : IMessage<T>, new()
    {
      var ret = new HashSet<T>();

      while (reader.Read())
      {
        ret.Add(map.Parse<T>(reader));
      }

      return ret;
    }

    public async static Task<ICollection<T>> ParseAsync<T>(this SqlDataReader reader, IMapper map) where T : IMessage<T>, new()
    {
      var ret = new HashSet<T>();

      while (await reader.ReadAsync().ConfigureAwait(false))
      {
        ret.Add(map.Parse<T>(reader));
      }

      return ret;
    }
  }
}