using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dal.Sp
{
  internal sealed class ReadWrite<T> : Base<T>, IWrite<T> where T : new()
  {
    private readonly IRead<T> SpRonly;

    public ReadWrite(UserClaim claim, SpInfo sp, SpInfo spReadOnly, ISpMappers mappers) : base(claim, sp, mappers)
    {
      SpRonly = (spReadOnly == null) ? null : new ReadOnly<T>(claim, spReadOnly, mappers);
    }

    public int Create(T obj) => SetParameter(obj) ? Create() : -1;

    public bool Update(T obj) => SetParameter(obj) && Update();

    public bool UpdateState(int id, int stateId)
    {
      return SetParameter(Read(id)) && SetParameter(Constant.STATE.Id(), stateId) && Update();
    }

    public bool Delete(int id) => SetParameter(Constant.ID, id) && Update();

    public T Read(int id) => SpRonly.Read(id) ?? default;

    public IEnumerable<T> Read(string value) => SpRonly?.Read(value);

    public IEnumerable<T> Read(string key, object value) => SpRonly?.Read(key, value);

    public async override Task<IEnumerable<T>> ReadAsync() => await SpRonly.ReadAsync().ConfigureAwait(false);

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