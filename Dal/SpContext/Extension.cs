using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading.Tasks;

using Dal.Sp;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Microsoft.Data.SqlClient;

namespace Dal
{
  public static class Extension
  {
    public static string Dot(this string str)
    {
      return string.Concat(str, Constant.DOT);
    }

    public static string DotAnd(this string str, string more)
    {
      return string.Concat(str.Dot(), more);
    }

    public static string Underscore(this string str)
    {
      return string.Concat(str, Constant.UNDERSCORE);
    }

    public static string UnderscoreAnd(this string str, string more)
    {
      return string.Concat(str.Underscore(), more);
    }

    public static string AsParameter(this string str)
    {
      return string.Concat(Constant.PARAMETERSYMBOL, str);
    }

    public static string Id(this string str)
    {
      return string.Concat(str, Constant.ID);
    }

    public static bool IsEqual(this string str, string compare, CultureInfo culture, bool ignoreCase)
    {
      return string.Compare(str, compare, ignoreCase, culture) == 0;
    }

    public static bool IsEqual(this string str, string compare)
    {
      return str.IsEqual(compare, CultureInfo.CurrentCulture, true);
    }

    public static string SpName(this Type t, string schema, string op)
    {
      return schema.DotAnd(t.Name).UnderscoreAnd(op);
    }

    public static SqlDbType ToSqlDbType(this string str)
    {
      if (string.IsNullOrEmpty(str))
        throw new ArgumentNullException();

      return str switch
      {
        "int" => SqlDbType.Int,
        "tinyint" => SqlDbType.TinyInt,
        "bit" => SqlDbType.Bit,
        "money" => SqlDbType.Money,
        "varchar" => SqlDbType.VarChar,
        "nvarchar" => SqlDbType.NVarChar,
        "char" => SqlDbType.Char,
        "float" => SqlDbType.Float,
        _ => throw new InvalidCastException()
      };
    }

    public static IEnumerable<T> Parse<T>(this SqlDataReader reader, IMapper map) where T : IMessage, new()
    {
      var ret = new HashSet<T>();

      while (reader.Read())
      {
        ret.Add(map.Parse<T>(reader));
      }

      return ret;
    }

    public async static Task<IEnumerable<T>> ParseAsync<T>(this SqlDataReader reader, IMapper map) where T : IMessage, new()
    {
      var ret = new HashSet<T>();

      while (await reader.ReadAsync().ConfigureAwait(false))
      {
        ret.Add(map.Parse<T>(reader));
      }

      return ret;
    }

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
  }
}