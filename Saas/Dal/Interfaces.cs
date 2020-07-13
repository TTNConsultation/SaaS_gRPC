using Microsoft.Data.SqlClient;
using Saas.Entity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Saas.Dal
{
  public interface ISpContext
  {
    IRonly<T> SpAppData<T>() where T : new();

    IRonly<T> SpROnly<T>(AppData appData, ClaimsPrincipal uc, OperationType op) where T : new();

    ICrud<T> SpCrud<T>(AppData appData, ClaimsPrincipal uc, OperationType op) where T : new();
  }

  public interface IRonly<T> : IDisposable where T : new()
  {
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

    bool UpdateState(int id, int stateId);

    bool Delete(int id);
  }

  internal interface ICollectionSpToEntity
  {
    public ISpToEntity FirstOrDefault(string typename);

    T Add<T>(SqlDataReader reader, out ISpToEntity mapper) where T : new();
  }

  internal interface ISpToEntity
  {
    bool Type(string typename);

    T Build<T>(SqlDataReader reader) where T : new();

    T Parse<T>(SqlDataReader reader) where T : new();
  }

  internal interface IConnectionStringManager
  {
    string GetConnectionString(string role);

    string GetConnectionString(int order);

    string GetAppConnectionString();
  }
}