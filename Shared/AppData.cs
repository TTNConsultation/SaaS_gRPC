﻿using DbContext.Interface;
using Protos.Shared.Message.Language;
using Protos.Shared.Message.Reference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Protos.Shared
{
  public class AppData
  {
    public readonly References RefDatas;
    public readonly DictionaryCache DictCache;

    public AppData(IDbContext context)
    {
      RefDatas = new References(context);
      DictCache = new DictionaryCache();
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