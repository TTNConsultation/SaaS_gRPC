﻿using Constant;
using System.Collections.Generic;
using System.Linq;

namespace Protos.Message.Language
{
  public partial class Keys
  {
    public Keys(ICollection<Key> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class CodeLanguage
  {
    public bool IsEqual(CodeLanguage lang) => Id == lang.Id;
  }

  public partial class SupportedLanguages
  {
    public SupportedLanguages(ICollection<CodeLanguage> values)
    {
      Values.AddRange(values);
    }

    public CodeLanguage Get(string code) => Values.FirstOrDefault(l => l.Code.IsEqual(code));

    public CodeLanguage Get(int id) => Values.FirstOrDefault(l => l.Id == id);
  }

  public partial class Dictionary
  {
    public bool IsEqual(Dictionary dict) => IsEqual(dict.RootId, dict.Language);

    public bool IsEqual(int rootId, CodeLanguage lang) => RootId == rootId && Language.IsEqual(lang);

    public string UniqueCode => string.Concat(RootId.ToString(), ".", Language.Code);

    public Dictionary(int rootId, CodeLanguage lang, Keys allkeys, ICollection<DictKeyValuePair> content)
    {
      RootId = rootId;
      Language = lang;
      Keys = allkeys;
      Content.AddRange(content);
    }

    public IDictionary<int, string> AsDictionary => Content.ToDictionary(c => c.Key, c => c.Val);
  }
}