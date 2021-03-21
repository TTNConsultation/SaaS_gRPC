﻿using Constant;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DbContext.Interfaces
{
  public enum OperationType { C, R, RR, U, D }

  public interface IDbContext
  {
    IExecuteReader<T> ReferenceData<T>(int rootId = 0) where T : IMessage<T>, new();

    IExecuteReader<T> GetReader<T>(int appId, ClaimsPrincipal uc, OperationType op = OperationType.R) where T : IMessage<T>, new();

    IExecuteNonQuery<T> GetWriter<T>(int appId, ClaimsPrincipal uc, OperationType op) where T : IMessage<T>, new();
  }

  public interface IExecuteNonQuery<T> : IDisposable where T : IMessage<T>, new()
  {
    string Error { get; }

    int RootId { get; }

    bool IsReady { get; }

    IExecuteReader<T> Reader { get; }

    bool AddParameter(string key, object value);

    bool AddParameter(T obj) =>
      (obj != null) && obj.Descriptor.Fields.InDeclarationOrder()
                                            .FirstOrDefault(fd => !AddParameter(fd.Name, fd.Accessor.GetValue(obj))) == null;

    int Create();

    bool Update();

    int Create(T obj) => AddParameter(obj) ? Create() : throw new Exception(StrVal.PARAMETERNONVALID);

    bool Update(T obj) => AddParameter(obj) && Update();

    bool UpdateState(int id, int stateId) =>
     AddParameter(Reader.Read(id)) && AddParameter(StrVal.STATE.AndId(), stateId) && Update();

    bool Delete(int id) => AddParameter(StrVal.ID, id) && Update();
  }

  public interface IExecuteReader<T> : IDisposable where T : IMessage<T>, new()
  {
    string Error { get; }

    int RootId { get; }

    bool IsReady { get; }

    bool AddParameter(string key, object value);

    bool AddParameter(IDictionary<string, object> parameters) =>
      parameters.FirstOrDefault(p => !AddParameter(p.Key, p.Value))
                .Equals(default(KeyValuePair<string, object>));

    ICollection<T> Read();

    T Read(int id)
    {
      var res = Read(StrVal.ID, id);
      return res.Count == 0 ? default : res.First();
    }

    ICollection<T> Read(string value) => Read(StrVal.VALUE, value);

    ICollection<T> Read(string key, object value) =>
      AddParameter(key, value) ? Read() : throw new Exception(StrVal.PARAMETERNONVALID);

    ICollection<T> Read(IDictionary<string, object> parameters) =>
      AddParameter(parameters) ? Read() : throw new Exception(StrVal.PARAMETERNONVALID);

    ICollection<T> ReadRange(string key, string values, char separator) =>
      AddParameter(key, values) && AddParameter(StrVal.SEPARATOR, separator) ? Read() : throw new Exception(StrVal.PARAMETERNONVALID);

    ICollection<T> ReadBy<S>(int sid) where S : IMessage<S> =>
      Read(typeof(S).Name.AndId(), sid);

    Task<ICollection<T>> ReadAsync();

    Task<ICollection<T>> ReadAsync(string value) =>
      ReadAsync(StrVal.VALUE, value);

    Task<ICollection<T>> ReadAsync(string key, object value) =>
      AddParameter(key, value) ? ReadAsync() : throw new Exception(StrVal.PARAMETERNONVALID);

    Task<ICollection<T>> ReadAsync(IDictionary<string, object> parameters) =>
      AddParameter(parameters) ? ReadAsync() : throw new Exception(StrVal.PARAMETERNONVALID);

    Task<ICollection<T>> ReadRangeAsync(string key, string values, char separator) =>
      AddParameter(key, values) && AddParameter(StrVal.SEPARATOR, separator) ? ReadAsync() : throw new Exception(StrVal.PARAMETERNONVALID);

    Task<ICollection<T>> ReadByAsync<S>(int sid) where S : IMessage<S> =>
      ReadAsync(typeof(S).Name.AndId(), sid);
  }
}