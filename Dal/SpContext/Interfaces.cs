using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dal.Sp
{
  public interface IContext
  {
    IRonly<T> SpAppData<T>() where T : new();

    IRonly<T> SpROnly<T>(IAppData appData, ClaimsPrincipal uc, OperationType op) where T : new();

    ICrud<T> SpCrud<T>(IAppData appData, ClaimsPrincipal uc, OperationType op) where T : new();
  }

  public interface IRonly<T> : IDisposable where T : new()
  {
    T Read(int id);

    IEnumerable<T> Read(string value);

    IEnumerable<T> Read(string key, object id);

    Task<T> ReadAsync(int id);

    Task<IEnumerable<T>> ReadAsync();

    Task<IEnumerable<T>> ReadAsync(string value);

    Task<IEnumerable<T>> ReadAsync(string key, object id);

    Task<IEnumerable<T>> ReadAsync(IDictionary<string, object> ids);

    Task<IEnumerable<T>> ReadRangeAsync(string key, string values, char separator);
  }

  public interface ICrud<T> : IRonly<T> where T : new()
  {
    int Create(T obj);

    bool Update(T obj);

    Task<bool> UpdateState(int id, int stateId);

    bool Delete(int id);
  }

  public interface IAppData
  {
    int AppId();
  }

  public interface ICollectionMapToEntity
  {
    public IMapToEntity FirstOrDefault(string type);

    T Add<T>(SqlDataReader reader, out IMapToEntity mapper) where T : new();
  }

  public interface IMapToEntity
  {
    bool IsType(string type);

    T Build<T>(SqlDataReader reader) where T : new();

    T Parse<T>(SqlDataReader reader) where T : new();
  }

  public interface IConnectionStringManager
  {
    string GetConnectionString(string role);

    string GetConnectionString(int order);

    string GetAppConnectionString();
  }
}