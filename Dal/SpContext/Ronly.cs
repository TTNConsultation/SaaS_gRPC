using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dal.Sp
{
  internal sealed class Ronly<T> : Base<T>, IRonly<T> where T : new()
  {
    public Ronly(User user, Info sp, ICollectionMapToEntity mappers, string conStr) : base(user, sp, mappers, conStr)
    {
    }

    public T Read(int id) => Read(Constant.ID, id).First();

    public IEnumerable<T> Read(string value) => Read(Constant.VALUE, value);

    public IEnumerable<T> Read(string key, object value) => AddParameter(key, value) ? Read() : null;

    public async Task<T> ReadAsync(int id) => await Task.FromResult(Read(Constant.ID, id).First()).ConfigureAwait(false);

    public async Task<IEnumerable<T>> ReadAsync(string value) => await ReadAsync(Constant.VALUE, value).ConfigureAwait(false);

    public async Task<IEnumerable<T>> ReadAsync(string key, object value) => AddParameter(key, value) ? await ReadAsync().ConfigureAwait(false) : null;

    public async Task<IEnumerable<T>> ReadAsync(IDictionary<string, object> parameters) => AddParameters(parameters) ? await ReadAsync().ConfigureAwait(false) : null;

    public async Task<IEnumerable<T>> ReadRangeAsync(string key, string values, char separator) => AddParameter(key, values) && AddParameter(Constant.SEPARATOR, separator) ? await ReadAsync().ConfigureAwait(false) : null;
  }
}