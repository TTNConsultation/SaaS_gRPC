using System;
using System.Data;
using System.Globalization;

namespace Constant
{
  public static class Extensions
  {
    public static string Dot(this string str) => string.Concat(str, StrVal.DOT);

    public static string DotAnd(this string str, string more) => string.Concat(str.Dot(), more);

    public static string AsId(this string str) => string.Concat(str, StrVal.ID);

    public static string AsParameter(this string str) => string.Concat(StrVal.PARAMETERSYMBOL, str);

    public static string Underscore(this string str) => string.Concat(str, StrVal.UNDERSCORE);

    public static string UnderscoreAnd(this string str, string more) =>
      string.Concat(str.Underscore(), more);

    public static bool IsEqual(this string str, string compare, CultureInfo culture, bool ignoreCase) =>
      string.Compare(str, compare, ignoreCase, culture) == 0;

    public static bool IsEqual(this string str, string compare) =>
      str.IsEqual(compare, CultureInfo.CurrentCulture, true);

    public static SqlDbType ToSqlDbType(this string str)
    {
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
  }
}