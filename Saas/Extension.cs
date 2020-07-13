using System.Globalization;

namespace Saas
{
  public static class Extension
  {
    public static string Id(this string str)
    {
      return string.Concat(str, "Id");
    }

    public static bool IsEqual(this string str, string compare, CultureInfo culture, bool ignoreCase)
    {
      return string.Compare(str, compare, ignoreCase, culture) == 0;
    }

    public static bool IsEqual(this string str, string compare)
    {
      return str.IsEqual(compare, CultureInfo.CurrentCulture, true);
    }
  }
}