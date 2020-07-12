using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dal.Sp
{
  public interface IReadOnly<T> : IDisposable where T : new()
  {
    bool IsReady();

    string Error();

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

  internal sealed class ReadOnly<T> : Base<T>, IReadOnly<T> where T : new()
  {
    public ReadOnly(Context.UserClaim claim, ISpInfo sp, ICollectionMapper mappers) : base(claim, sp, mappers)
    {
    }

    public T Read(int id) => Read(Constant.ID, id).First();

    public IEnumerable<T> Read(string value) => Read(Constant.VALUE, value);

    public IEnumerable<T> Read(string key, object value) => AddParameter(key, value) ? Read() : null;

    public async Task<IEnumerable<T>> ReadAsync(string value) => await ReadAsync(Constant.VALUE, value).ConfigureAwait(false);

    public async Task<IEnumerable<T>> ReadAsync(string key, object value) => AddParameter(key, value) ? await ReadAsync().ConfigureAwait(false) : null;

    public async Task<IEnumerable<T>> ReadAsync(IDictionary<string, object> parameters) => AddParameters(parameters) ? await ReadAsync().ConfigureAwait(false) : null;

    public async Task<IEnumerable<T>> ReadRangeAsync(string key, string values, char separator) => AddParameter(key, values) && AddParameter(Constant.SEPARATOR, separator) ? await ReadAsync().ConfigureAwait(false) : null;
  }
}