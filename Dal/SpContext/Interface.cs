using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

using Microsoft.Data.SqlClient;
using Google.Protobuf;

namespace StoreProcedure.Interface
{
  public enum OperationType { C, R, RR, U, D, ND }

  public interface IDbContext
  {
    IExecuteReader<T> ReferenceData<T>(int rootId = 0) where T : IMessage, new();

    IExecuteReader<T> Read<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : IMessage, new();

    IExecuteNonQuery<T> Write<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : IMessage, new();
  }

  public interface ICollectionStoreProcedure
  {
    IStoreProcedure Get(string typename, OperationType op);

    IStoreProcedure Get<T>(OperationType op) => Get(typeof(T).Name, op);
  }

  public interface IStoreProcedure
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

  public interface IExecuteNonQuery<T> : IDisposable where T : IMessage, new()
  {
    string Error { get; }

    int RootId { get; }

    bool IsReady { get; }

    int Create(T obj);

    bool Update(T obj);

    bool UpdateState(int id, int stateId);

    bool Delete(int id);
  }

  public interface IExecuteReader<T> : IDisposable where T : IMessage, new()
  {
    string Error { get; }

    int RootId { get; }

    bool IsReady { get; }

    T Read(int id) => Read(Constant.ID, id).First();

    IEnumerable<T> Read();

    IEnumerable<T> ReadBy<S>(int id) => Read(typeof(S).Name.Id(), id);

    IEnumerable<T> Read(string value) => Read(Constant.VALUE, value);

    IEnumerable<T> Read(string key, object value);

    IEnumerable<T> Read(IDictionary<string, object> parameters);

    IEnumerable<T> ReadRange(string key, string values, char separator);

    Task<IEnumerable<T>> ReadAsync();

    Task<IEnumerable<T>> ReadAsyncBy<S>(int id) => ReadAsync(typeof(S).Name.Id(), id);

    Task<IEnumerable<T>> ReadAsync(string value) => ReadAsync(Constant.VALUE, value);

    Task<IEnumerable<T>> ReadAsync(string key, object id);

    Task<IEnumerable<T>> ReadAsync(IDictionary<string, object> ids);

    Task<IEnumerable<T>> ReadRangeAsync(string key, string values, char separator);
  }

  public interface IConnectionManager
  {
    string Get(string schema);

    string App() => Get(Constant.APP);
  }

  public interface ICollectionMapper
  {
    IMapper Get(string typeName);

    IMapper Get<T>() where T : IMessage => Get(typeof(T).Name);
  }

  public interface IMapper
  {
    string TypeName { get; }

    bool IsEqual(IMapper map) => TypeName.IsEqual(map.TypeName);

    T Parse<T>(SqlDataReader reader) where T : IMessage, new();
  }

}