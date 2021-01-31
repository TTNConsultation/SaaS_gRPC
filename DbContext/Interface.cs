using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

using Microsoft.Data.SqlClient;
using Google.Protobuf;

namespace DbContext.Interface
{
  public enum OperationType { C, R, RR, U, D }

  public interface IDbContext
  {
    IExecuteReader<T> ReferenceData<T>(int rootId = 0) where T : IMessage<T>, new();

    IExecuteReader<T> Read<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : IMessage<T>, new();

    IExecuteNonQuery<T> Write<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : IMessage<T>, new();
  }

  public interface ICollectionProcedure
  {
    IProcedure Get(string typename, OperationType op);

    IProcedure Get<T>(OperationType op) => Get(typeof(T).Name, op);
  }

  public interface IProcedure
  {
    SqlCommand SqlCommand(string conStr);

    IParameter Parameter(string name);

    bool IsEqual(string spName, OperationType op);
  }

  public interface IParameter
  {
    string ParameterName { get; }

    int ProcedureId { get; }

    SqlParameter SqlParameter(object value);
  }

  public interface IExecuteNonQuery<T> : IDisposable where T : IMessage<T>, new()
  {
    string Error { get; }

    int RootId { get; }

    bool IsReady { get; }

    int Create(T obj);

    bool Update(T obj);

    bool UpdateState(int id, int stateId);

    bool Delete(int id);
  }

  public interface IExecuteReader<T> : IDisposable where T : IMessage<T>, new()
  {
    string Error { get; }

    int RootId { get; }

    bool IsReady { get; }

    T Read(int id) => Read(Constant.ID, id).First();

    ICollection<T> Read();

    ICollection<T> ReadBy<S>(int id) => Read(nameof(S).AsId(), id);

    ICollection<T> Read(string value) => Read(Constant.VALUE, value);

    ICollection<T> Read(string key, object value);

    ICollection<T> Read(IDictionary<string, object> parameters);

    ICollection<T> ReadRange(string key, string values, char separator);

    Task<ICollection<T>> ReadAsync();

    Task<ICollection<T>> ReadAsyncBy<S>(int id) => ReadAsync(nameof(S).AsId(), id);

    Task<ICollection<T>> ReadAsync(string value) => ReadAsync(Constant.VALUE, value);

    Task<ICollection<T>> ReadAsync(string key, object value);

    Task<ICollection<T>> ReadAsync(IDictionary<string, object> parameters);

    Task<ICollection<T>> ReadRangeAsync(string key, string values, char separator);
  }

  public interface IConnectionManager
  {
    string Get(string schema);

    string App(string appSchema = Constant.APP) => Get(appSchema);
  }

  public interface ICollectionMapper
  {
    IMapper Get(Type type);

    IMapper Get<T>() where T : IMessage<T> => Get(typeof(T));
  }

  public interface IMapper
  {
    bool IsType(Type type);

    T Parse<T>(SqlDataReader reader) where T : IMessage<T>, new();
  }
}