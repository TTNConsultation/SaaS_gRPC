using Dal.Sp;
using System.Collections.Generic;
using System.Linq;

using static Saas.Entity.ReferenceData.KeyTypes.Types;
using static Saas.Entity.ReferenceData.States.Types;
using static Saas.Entity.ReferenceData.SupportedLanguages.Types;

namespace Saas.Entity.ReferenceData
{
  public partial class States
  {
    public int DeleteId => Values.First(s => s.Name.IsEqual("Delete")).Id;
    public int EnableId => Values.First(s => s.Name.IsEqual("Enable")).Id;
    public int DisableId => Values.First(s => s.Name.IsEqual("Disable")).Id;

    public States(IEnumerable<Types.State> values) => Values.AddRange(values);
  }

  public partial class SupportedLanguages
  {
    public SupportedLanguages(IEnumerable<CodeLanguage> values)
    {
      Values.AddRange(values);
    }

    public CodeLanguage Get(string code) => Values.FirstOrDefault(l => l.Code.IsEqual(code));

    public CodeLanguage Get(int id) => Values.FirstOrDefault(l => l.Id == id);

    public partial class Types
    {
      public partial class CodeLanguage
      {
        public bool IsEqual(CodeLanguage lang) => Id == lang.Id;
      }
    }
  }

  public partial class KeyTypes
  {
    public KeyTypes(IEnumerable<Types.KeyType> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class ReferenceDatas
  {
    public ReferenceDatas(IContext context)
    {
      App = context.ReferenceData<AppSetting>().Read().First();
      States = new States(context.ReferenceData<State>(App.Id).Read());
      Languages = new SupportedLanguages(context.ReferenceData<CodeLanguage>(App.Id).Read());
      KeyTypes = new KeyTypes(context.ReferenceData<KeyType>(App.Id).Read());
    }
  }
}