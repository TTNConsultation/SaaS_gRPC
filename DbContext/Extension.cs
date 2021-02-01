using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
      switch (fd.FieldType)
      {
        case FieldType.Double:
        case FieldType.Float:
          return Convert.ChangeType(obj, typeof(float));

        case FieldType.Int64:
        case FieldType.SFixed64:
        case FieldType.SInt64:
          return Convert.ChangeType(obj, typeof(long));

        case FieldType.UInt64:
        case FieldType.Fixed64:
          return Convert.ChangeType(obj, typeof(ulong));

        case FieldType.Int32:
        case FieldType.SFixed32:
        case FieldType.SInt32:
          return Convert.ChangeType(obj, typeof(int));

        case FieldType.Fixed32:
        case FieldType.UInt32:
          return Convert.ChangeType(obj, typeof(uint));

        case FieldType.Bool:
          return Convert.ChangeType(obj, typeof(bool));

        case FieldType.String:
          return Convert.ChangeType(obj, typeof(string));

        case FieldType.Bytes:
          return Convert.ChangeType(obj, typeof(ByteString));

        default:
          return obj;
      }
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