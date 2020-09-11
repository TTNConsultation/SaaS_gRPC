using Microsoft.Data.SqlClient;

namespace Dal.Sp
{
  public interface ICollectionSpProperty
  {
    ISpProperty Get(string typename, OperationType op);

    ISpProperty Get<T>(OperationType op) => Get(typeof(T).Name, op);
  }

  public interface ISpProperty
  {
    SqlCommand SqlCommand(string conStr);

    IParameter Parameter(string name);

    bool IsEqual(string spName, OperationType op);
  }

  public interface IParameter
  {
    string ParameterName { get; }

    int StoreProcedureId { get; }

    SqlParameter SqlParameter(object value);
  }
}