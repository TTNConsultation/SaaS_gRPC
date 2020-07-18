using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Dal.Sp;
using Saas.Entity.Language;
using Saas.Entity.ReferenceData;

using static Saas.Entity.Language.Keys.Types;
using static Saas.Entity.ReferenceData.SupportedLanguages.Types;

namespace Saas.Entity
{
  internal class AppData
  {
    internal readonly ReferenceDatas RefDatas;
    internal readonly DictionaryCache DictCache;

    public AppData(IContext context)
    {
      RefDatas = new ReferenceDatas(context);
      DictCache = new DictionaryCache();
    }
  }

  internal class DictionaryCache
  {
    public IDictionary<string, Dictionary> Cache { get; } = new Dictionary<string, Dictionary>();

    public Dictionary Get(int rootId, CodeLanguage lang) => Get(string.Concat(rootId.ToString(), ".", lang.Code));

    public Dictionary Get(string uniqueCode) => Cache[uniqueCode];

    public Dictionary Add(Dictionary dict, bool overwrite = true)
    {
      if (Cache.ContainsKey(dict.UniqueCode) && !overwrite)
        return null;

      Cache.Remove(dict.UniqueCode);
      Cache.Add(dict.UniqueCode, dict);

      return Cache[dict.UniqueCode];
    }

    public async Task<Keys> GetKeys(int rootId, IContext context)
    {
      using var sp = context.ReferenceData<Key>(rootId);

      return await Task.FromResult(Cache.FirstOrDefault(c => c.Value.RootId == rootId).Value.Keys ??
        (sp.IsReady() ? new Keys(await sp.ReadAsync().ConfigureAwait(false)) : throw new NotSupportedException())).ConfigureAwait(false);
    }
  }
}