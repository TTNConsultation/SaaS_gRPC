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

    public Dictionary(int rootId, CodeLanguage lang, Keys allkeys, IEnumerable<DictKeyValuePair> content)
    {
      RootId = rootId;
      Language = lang;
      Keys = allkeys;
      Content.AddRange(content);
    }

    public IDictionary<int, string> AsDictionary => Content.ToDictionary(c => c.Key, c => c.Val);
  }

  public partial class DictKeyValuePair
  {
    public KeyValuePair<int, string> AsKeyValuePair => new KeyValuePair<int, string>(Key, Val);
  }
}