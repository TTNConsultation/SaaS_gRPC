using Microsoft.Data.SqlClient;

namespace Dal.Sp
{
  public interface ICollectionSpProperty
  {
    IStoreProcedure Get(string typename, OperationType op);

    IStoreProcedure Get<T>(OperationType op) => Get(typeof(T).Name, op);
  }

  public interface IStoreProcedure
  {
    SqlCommand SqlCommand(string conStr);

    IStoreProcedureParameter Parameter(string name);

    bool IsEqual(string spName, OperationType op);
  }

  public interface IStoreProcedureParameter
  {
    string ParameterName { get; }

    int StoreProcedureId { get; }

    SqlParameter SqlParameter(object value);
  }
}