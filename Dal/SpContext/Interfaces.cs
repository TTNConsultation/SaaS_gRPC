using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dal.Sp
{
  public interface IContext
  {
    IRead<T> ReferenceData<T>(int appId = 0) where T : new();

    IRead<T> ReadOnly<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : new();

    IWrite<T> ReadWrite<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : new();
  }

  public interface IRead<T> : IDisposable where T : new()
  {
    bool IsInit();

    T Read(int id);

    IEnumerable<T> Read();

    IEnumerable<T> Read(string value);

    IEnumerable<T> Read(string key, object id);

    Task<IEnumerable<T>> ReadAsync();

    Task<IEnumerable<T>> ReadAsync(string value);

    Task<IEnumerable<T>> ReadAsync(string key, object id);

    Task<IEnumerable<T>> ReadAsync(IDictionary<string, object> ids);

    Task<IEnumerable<T>> ReadRangeAsync(string key, string values, char separator);
  }

  public interface IWrite<T> : IRead<T> where T : new()
  {
    int Create(T obj);

    bool Update(T obj);

    bool UpdateState(int id, int stateId);

    bool Delete(int id);
  }

  public interface ISpMappers
  {
    public ISpMapper FirstOrDefault(string type);

    T Add<T>(SqlDataReader reader, out ISpMapper mapper) where T : new();
  }

  public interface ISpMapper
  {
    bool IsType(string type);

    T Build<T>(SqlDataReader reader) where T : new();

    T Parse<T>(SqlDataReader reader) where T : new();
  }

  internal interface IConnectionStringManager
  {
    string Get(string schema);

    ConnectionStringManager.User GetAppUser(int appId);

    ConnectionStringManager.User GetUser(ClaimsPrincipal uc, int appId);
  }
}