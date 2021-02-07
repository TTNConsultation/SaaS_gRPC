using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;

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
  }
}