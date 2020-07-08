using Dal.Sp;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading.Tasks;

namespace Dal
{
  public static class Extension
  {
    public static string Dot(this string str)
    {
      return str + Constant.DOT;
    }

    public static string DotAnd(this string str, string more)
    {
      return str.Dot() + more;
    }

    public static string Underscore(this string str)
    {
      return str + Constant.UNDERSCORE;
    }

    public static string UnderscoreAnd(this string str, string more)
    {
      return str.Underscore() + more;
    }

    public static string AsParameter(this string str)
    {
      return Constant.PARAMETERSYMBOL + str;
    }

    public static string Id(this string str)
    {
      return str + Constant.ID;
    }

    public static bool IsEqual(this string str, string compare, CultureInfo culture, bool ignoreCase = true)
    {
      return string.Compare(str, compare, ignoreCase, culture) == 0;
    }

    public static bool IsEqual(this string str, string compare)
    {
      return str.IsEqual(compare, CultureInfo.CurrentCulture, true);
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
        _ => throw new InvalidCastException()
      };
    }

    public async static Task<IEnumerable<T>> ParseAsync<T>(this SqlDataReader reader, ICollectionMapToEntity mappers) where T : new()
    {
      var ret = new HashSet<T>();
      var map = mappers.FirstOrDefault(typeof(T).Name);

      while (await reader.ReadAsync().ConfigureAwait(false))
      {
        ret.Add((map == null) ? mappers.Add<T>(reader, out map) : map.Parse<T>(reader));
      }

      return ret;
    }
  }
}