using System;
using System.Globalization;

namespace Utility
{
  public static class Extensions
  {
    public static string Dot(this string str) => string.Concat(str, STR.DOT);

    public static string DotAnd(this string str, string more) => string.Concat(str.Dot(), more);

    public static string AndId(this string str) => string.Concat(str, STR.ID);

    public static string AsParameter(this string str) => string.Concat(STR.PARAMETERSYMBOL, str);

    public static string Underscore(this string str) => string.Concat(str, STR.UNDERSCORE);

    public static string UnderscoreAnd(this string str, string more) =>
      string.Concat(str.Underscore(), more);

    public static bool IsEqual(this string str, string compare, CultureInfo culture, bool ignoreCase) =>
      string.Compare(str, compare, ignoreCase, culture) == 0;

    public static bool IsEqual(this string str, string compare) =>
      str.IsEqual(compare, CultureInfo.InvariantCulture, true);
  }
}