using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protos.Shared
{
  public static class Extensions
  {
    public static string Dot(this string str) => string.Concat(str, Constant.DOT);

    public static string DotAnd(this string str, string more) => string.Concat(str.Dot(), more);

    public static string AsId(this string str) => string.Concat(str, Constant.ID);

    public static string AsParameter(this string str) => string.Concat(Constant.PARAMETERSYMBOL, str);

    public static string Underscore(this string str) => string.Concat(str, Constant.UNDERSCORE);

    public static string UnderscoreAnd(this string str, string more) =>
      string.Concat(str.Underscore(), more);

    public static bool IsEqual(this string str, string compare, CultureInfo culture, bool ignoreCase) =>
      string.Compare(str, compare, ignoreCase, culture) == 0;

    public static bool IsEqual(this string str, string compare) =>
      str.IsEqual(compare, CultureInfo.CurrentCulture, true);

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
  }
}