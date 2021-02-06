using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Protos.Message.Reference;
using DbContext.Interfaces;
using Protos.Message.Language;

namespace Saas
{
  public class AppData
  {
    public readonly References RefDatas = new References();
    public readonly DictionaryCache DictCache = new DictionaryCache();

    public AppData(IDbContext context)
    {
      using var appSettingCtx = context.ReferenceData<AppSetting>();
      RefDatas.AppSetting = (appSettingCtx.IsReady) ? appSettingCtx.Read().First() : throw new NotSupportedException();

      using var stateCtx = context.ReferenceData<State>();
      RefDatas.States = (stateCtx.IsReady) ? new States(stateCtx.Read()) : throw new NotSupportedException();

      using var codeLanguageCtx = context.ReferenceData<CodeLanguage>();
      RefDatas.Languages = (codeLanguageCtx.IsReady) ? new SupportedLanguages(codeLanguageCtx.Read()) : throw new NotSupportedException();

      using var keyTypeCtx = context.ReferenceData<KeyType>();
      RefDatas.KeyTypes = (keyTypeCtx.IsReady) ? new KeyTypes(keyTypeCtx.Read()) : throw new NotSupportedException();
    }
  }

  public class DictionaryCache
  {
    public IDictionary<string, Dictionary> Cache { get; } = new Dictionary<string, Dictionary>();

    public Dictionary Get(int rootId, CodeLanguage lang)
    {
      var codeunique = string.Concat(rootId.ToString(), ".", lang.Code);
      return Get(codeunique) ?? new Dictionary();
    }

    public Dictionary Get(string uniqueCode) => Cache[uniqueCode];

    public Dictionary Add(Dictionary dict, bool overwrite = true)
    {
      if (!Cache.ContainsKey(dict.UniqueCode) || overwrite)
      {
        Cache.Remove(dict.UniqueCode);
        Cache.Add(dict.UniqueCode, dict);
      }

      return Cache[dict.UniqueCode];
    }

    public async Task<Keys> GetKeys(int rootId, IDbContext context)
    {
      using var sp = context.ReferenceData<Key>(rootId);

      return await Task.FromResult(Cache.FirstOrDefault(c => c.Value.RootId == rootId).Value.Keys ??
        (sp.IsReady ? new Keys(await sp.ReadAsync().ConfigureAwait(false)) : throw new NotSupportedException())).ConfigureAwait(false);
    }
  }
}