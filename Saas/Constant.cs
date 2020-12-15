using StoreProcedure;

namespace Saas
{
  internal static class Constant
  {
    public static string FullName<T>() => typeof(T).FullName;

    public static string TypeName<T>() => typeof(T).Name;

    public static string TypeNameId<T>() => TypeName<T>().Id();

    public const string UNDERSCORE = "_";
    public const string DOT = ".";
    public const string COMA = ",";
    public const char COMA_ = ',';

    public const string PARAMETERSYMBOL = "@";
    public const string RETVAL = "retVal";
    public const string ID = "id";
    public const string VALUE = "value";
    public const string CODE = "code";
    public const string STATE = "state";

    public const string APP = "app";
    public const string ADMINISTRATOR = "administrator";
    public const string EMPLOYEE = "employee";
    public const string USER = "user";

    public const string ROOT = "root";
    public const string ROLE = "role";

    public const string ERR = "err";
    public const string SEPARATOR = "separator";

    public const string ENABLE = "enable";
    public const string DISABLE = "disable";

    public const string CREATE = "C";
    public const string READ = "R";
    public const string UPDATE = "U";
    public const string DELETE = "D";
    public const string LOOKUP = "L";

    public const string NULL = "null";

    public const string TINYINT = "statekeytypetablelanguage";
    public const string PRICE = "price";
    public const string DATE = "date";
    public const string TIME = "time";

    public const string CorsAllowedPolicy = "CorsAllowed";

    //Config section
    public const string DAL = "Dal";

    public const string DAL_CONNECTIONSTRINGS = "ConnectionStrings";
    public const string DAL_APPID = "AppId";
  }
}