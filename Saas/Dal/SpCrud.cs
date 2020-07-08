using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Saas.Security;

namespace Saas.Dal
{
  internal sealed class SpCrud<T> : SpBase<T>, ICrud<T> where T : new()
  {
    private readonly IRonly<T> SpRonly;

    public SpCrud(User user, SpInfo sp, SpInfo spReadOnly, ICollectionSpToEntity mappers, string conStr) : base(user, sp, mappers, conStr)
    {
      if (spReadOnly != null)
        SpRonly = new SpRonly<T>(user, spReadOnly, mappers, conStr);
    }

    public int Create(T obj)
    {
      return AddParameters(obj) ? Create() : -1;
    }

    public bool Update(T obj)
    {
      return AddParameters(obj) && UpdateDelete();
    }

    public bool UpdateState(int id, int stateId)
    {
      var obj = ReadAsync(id).Result;
      return AddParameters(obj) && SetParameter(Constant.STATE.Id(), stateId) && UpdateDelete();
    }

    public bool Delete(int id)
    {
      return AddParameter(Constant.ID, id) && UpdateDelete();
    }

    public async Task<T> ReadAsync(int id) => (SpRonly == null) ? throw new NullReferenceException() : await SpRonly.ReadAsync(id).ConfigureAwait(false);

    public async override Task<IEnumerable<T>> ReadAsync()
    {
      return (SpRonly == null) ? throw new NullReferenceException()
                               : await SpRonly.ReadAsync().ConfigureAwait(false);
    }

    public async Task<IEnumerable<T>> ReadAsync(string value)
    {
      return (SpRonly == null) ? throw new NullReferenceException()
                               : await SpRonly.ReadAsync(value).ConfigureAwait(false);
    }

    public async Task<IEnumerable<T>> ReadAsync(string key, object id)
    {
      return (SpRonly == null) ? throw new NullReferenceException()
                               : await SpRonly.ReadAsync(key, id).ConfigureAwait(false);
    }

    public async Task<IEnumerable<T>> ReadAsync(IDictionary<string, object> ids)
    {
      return (SpRonly == null) ? throw new NullReferenceException()
                               : await SpRonly.ReadAsync(ids).ConfigureAwait(false);
    }

    public async Task<IEnumerable<T>> ReadRangeAsync(string key, string values, char separator)
    {
      return (SpRonly == null) ? throw new NullReferenceException()
                               : await SpRonly.ReadRangeAsync(key, values, separator).ConfigureAwait(false);
    }

    public override void Dispose()
    {
      SpRonly?.Dispose();
      base.Dispose();
    }
  }
}