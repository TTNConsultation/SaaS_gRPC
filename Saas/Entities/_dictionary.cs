using System.Collections.Generic;
using System.Linq;
using static Saas.Entity.ReferenceData.SupportedLanguages.Types;

namespace Saas.Entity.Language
{
  public partial class Keys
  {
    public Keys(IEnumerable<Types.Key> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class Dictionary
  {
    public bool IsEqual(Dictionary dict) => IsEqual(dict.RootId, dict.Language);

    public bool IsEqual(int rootId, CodeLanguage lang) => RootId == rootId && Language.IsEqual(lang);

    public string UniqueCode => string.Concat(RootId.ToString(), ".", Language.Code);

    public Dictionary(Keys allkeys, int rootId, CodeLanguage lang, IEnumerable<DictKeyValuePair> content)
    {
      RootId = rootId;
      Language = lang;
      Keys = allkeys;
      Content.AddRange(content);
    }

    public IDictionary<int, string> AsDictionary => Content.Select(c => c.AsKeyValuePair).ToDictionary(k => k.Key, k => k.Value);
  }

  public partial class DictKeyValuePair
  {
    public bool IsEqual(DictKeyValuePair dictkvp) => this.AsKeyValuePair.Equals(dictkvp.AsKeyValuePair);

    public KeyValuePair<int, string> AsKeyValuePair => new KeyValuePair<int, string>(Key, Val);
  }
}