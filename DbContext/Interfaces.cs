using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Utility;
using Google.Protobuf;

namespace DbContext.Interfaces
{
  public enum OperationType { C, R, RR, U, D }

  public interface IDbContext
  {
    IReader<T> GetAppReader<T>(OperationType op = OperationType.R) where T : IMessage<T>, new();

    IReader<T> GetReader<T>(ClaimsPrincipal uc, OperationType op = OperationType.R) where T : IMessage<T>, new();

    IWriter<T> GetWriter<T>(ClaimsPrincipal uc, OperationType op) where T : IMessage<T>, new();
  }

  public interface IWriter<T> where T : IMessage<T>, new()
  {
    bool IsValid { get; }

    IReader<T> Reader { get; }

    //IWriter<T> Clone();

    bool AddParameter(string key, object value);

    bool AddParameter(IMessage obj);

    int ExecuteNonQuery();

    object ExecuteScalar();

    object Create(IMessage obj) => AddParameter(obj) ? ExecuteScalar() : throw new Exception(STR.PARAMETERNONVALID);

    bool Update(IMessage obj) => AddParameter(obj) && ExecuteNonQuery() == 1;

    bool UpdateState(int id, int stateId) =>
     AddParameter(Reader.First(id)) && AddParameter(STR.STATE.AndId(), stateId) && ExecuteNonQuery() == 1;

    bool Delete(int id) => AddParameter(STR.ID, id) && ExecuteNonQuery() == 1;
  }

  public interface IReader<T> where T : IMessage<T>, new()
  {
    bool IsValid { get; }

    bool AddParameter(string key, object value);

    bool AddParameter(IMessage obj);

    bool AddParameter(IDictionary<string, object> par);

    //IReader<T> Clone();

    ICollection<T> ExecuteReader();

    T First(int id) => Read(STR.ID, id).FirstOrDefault();

    T First(string value) => Read(value).FirstOrDefault();

    T First(IMessage obj) => Read(obj).FirstOrDefault();

    ICollection<T> Read(string value) => Read(STR.VALUE, value);

    ICollection<T> Read(string key, object value) =>
      AddParameter(key, value) ? ExecuteReader() : throw new Exception(STR.PARAMETERNONVALID);

    ICollection<T> Read(IMessage obj) =>
      AddParameter(obj) ? ExecuteReader() : throw new Exception(STR.PARAMETERNONVALID);

    ICollection<T> Read(IDictionary<string, object> par) =>
      AddParameter(par) ? ExecuteReader() : throw new Exception(STR.PARAMETERNONVALID);

    ICollection<T> ReadRange(string key, string values, char separator) =>
      AddParameter(key, values) && AddParameter(STR.SEPARATOR, separator) ? ExecuteReader() : throw new Exception(STR.PARAMETERNONVALID);

    ICollection<T> ReadBy<S>(int sid) where S : IMessage<S> =>
      Read(typeof(S).Name.AndId(), sid);

    Task<ICollection<T>> ExecuteAsync();

    Task<ICollection<T>> ReadAsync(string value) =>
      ReadAsync(STR.VALUE, value);

    Task<ICollection<T>> ReadAsync(string key, object value) =>
      AddParameter(key, value) ? ExecuteAsync() : throw new Exception(STR.PARAMETERNONVALID);

    Task<ICollection<T>> ReadAsync(IMessage obj) =>
      AddParameter(obj) ? ExecuteAsync() : throw new Exception(STR.PARAMETERNONVALID);

    Task<ICollection<T>> ReadRangeAsync(string key, string values, char separator) =>
      AddParameter(key, values) && AddParameter(STR.SEPARATOR, separator) ? ExecuteAsync() : throw new Exception(STR.PARAMETERNONVALID);

    Task<ICollection<T>> ReadByAsync<S>(int sid) where S : IMessage<S> =>
      ReadAsync(typeof(S).Name.AndId(), sid);
  }
}