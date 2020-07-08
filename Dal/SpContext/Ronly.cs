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

    public async Task<T> ReadAsync(int id)
    {
      return await Task.FromResult(ReadAsync(Constant.ID, id).Result.First()).ConfigureAwait(false);
    }

    public async Task<IEnumerable<T>> ReadAsync(string value)
    {
      return await ReadAsync(Constant.VALUE, value).ConfigureAwait(false);
    }

    public async Task<IEnumerable<T>> ReadAsync(string parameterName, object parameterValue)
    {
      return AddParameter(parameterName, parameterValue) ? await ReadAsync().ConfigureAwait(false) : null;
    }

    public async Task<IEnumerable<T>> ReadAsync(IDictionary<string, object> parameters)
    {
      return AddParameters(parameters) ? await ReadAsync().ConfigureAwait(false) : null;
    }

    public async Task<IEnumerable<T>> ReadRangeAsync(string parameterName, string values, char separator)
    {
      return AddParameter(parameterName, values) && AddParameter(Constant.SEPARATOR, separator) ? await ReadAsync().ConfigureAwait(false) : null;
    }
  }
}