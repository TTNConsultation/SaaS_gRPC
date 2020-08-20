﻿using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dal.Sp
{
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

  internal sealed class ExecuteReader<T> : DbCommand<T>, IExecuteReader<T> where T : IMessage, new()
  {
    public ExecuteReader(DbContext.UserClaim claim, ISpProperty sp, ICollectionMapper reflectionMaps) : base(claim, sp, reflectionMaps)
    {
    }

    public IEnumerable<T> Read(string key, object value) => AddParameter(key, value) ? Read() : null;

    public IEnumerable<T> Read(IDictionary<string, object> parameters) => AddParameters(parameters) ? Read() : null;

    public IEnumerable<T> ReadRange(string key, string values, char separator) => AddParameter(key, values) && AddParameter(Constant.SEPARATOR, separator) ? Read() : null;

    public async Task<IEnumerable<T>> ReadAsync(string key, object value) => AddParameter(key, value) ? await ReadAsync().ConfigureAwait(false) : null;

    public async Task<IEnumerable<T>> ReadAsync(IDictionary<string, object> parameters) => AddParameters(parameters) ? await ReadAsync().ConfigureAwait(false) : null;

    public async Task<IEnumerable<T>> ReadRangeAsync(string key, string values, char separator) => AddParameter(key, values) && AddParameter(Constant.SEPARATOR, separator) ? await ReadAsync().ConfigureAwait(false) : null;
  }
}