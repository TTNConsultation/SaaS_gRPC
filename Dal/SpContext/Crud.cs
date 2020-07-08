using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dal.Sp
{
  internal sealed class Crud<T> : Base<T>, ICrud<T> where T : new()
  {
    private readonly IRonly<T> SpRonly;

    public Crud(User user, Info sp, Info spReadOnly, ICollectionMapToEntity mappers, string conStr) : base(user, sp, mappers, conStr)
    {
      SpRonly = (spReadOnly == null) ? null : new Ronly<T>(user, spReadOnly, mappers, conStr);
    }

    public int Create(T obj) => AddParameters(obj) ? Create() : -1;

    public bool Update(T obj) => AddParameters(obj) && Update();

    public async Task<bool> UpdateState(int id, int stateId)
    {
      return AddParameters(await ReadAsync(id).ConfigureAwait(false)) && SetParameter(Constant.STATE.Id(), stateId) && Update();
    }

    public bool Delete(int id) => AddParameter(Constant.ID, id) && Update();

    public T Read(int id) => (SpRonly == null) ? throw new NullReferenceException() : SpRonly.Read(id);

    public IEnumerable<T> Read(string value) => (SpRonly == null) ? throw new NullReferenceException() : SpRonly.Read(value);

    public IEnumerable<T> Read(string key, object value) => (SpRonly == null) ? throw new NullReferenceException() : SpRonly.Read(key, value);

    public async Task<T> ReadAsync(int id) => (SpRonly == null) ? throw new NullReferenceException() : await SpRonly.ReadAsync(id).ConfigureAwait(false);

    public async override Task<IEnumerable<T>> ReadAsync()
    {
      return (SpRonly == null) ? throw new NullReferenceException()
                               : await SpRonly.ReadAsync().ConfigureAwait(false);
    }

    public async Task<IEnumerable<T>> ReadAsync(string value) => await SpRonly.ReadAsync(value).ConfigureAwait(false) ?? throw new NullReferenceException();

    public async Task<IEnumerable<T>> ReadAsync(string key, object id) => await SpRonly.ReadAsync(key, id).ConfigureAwait(false) ?? throw new NullReferenceException();

    public async Task<IEnumerable<T>> ReadAsync(IDictionary<string, object> ids) => await SpRonly.ReadAsync(ids).ConfigureAwait(false) ?? throw new NullReferenceException();

    public async Task<IEnumerable<T>> ReadRangeAsync(string key, string values, char separator) => await SpRonly.ReadRangeAsync(key, values, separator).ConfigureAwait(false) ?? throw new NullReferenceException();

    public override void Dispose()
    {
      SpRonly?.Dispose();
      base.Dispose();
    }
  }
}