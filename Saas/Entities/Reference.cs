using Dal.Sp;
using System.Collections.Generic;
using System.Linq;

using static Saas.Entity.Reference.KeyTypes.Types;
using static Saas.Entity.Reference.States.Types;
using static Saas.Entity.Language.SupportedLanguages.Types;
using Saas.Entity.Language;

namespace Saas.Entity.Reference
{
  public partial class States
  {
    public int DeleteId => Values.First(s => s.Name.IsEqual("Delete")).Id;
    public int EnableId => Values.First(s => s.Name.IsEqual("Enable")).Id;
    public int DisableId => Values.First(s => s.Name.IsEqual("Disable")).Id;

    public States(IEnumerable<State> values) => Values.AddRange(values);
  }

  public partial class KeyTypes
  {
    public KeyTypes(IEnumerable<KeyType> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class References
  {
    internal readonly AppSetting AppSetting;

    public References(IDbContext context)
    {
      AppSetting = context.ReferenceData<AppSetting>().Read().First();
      States = new States(context.ReferenceData<State>().Read());
      Languages = new SupportedLanguages(context.ReferenceData<CodeLanguage>().Read());
      KeyTypes = new KeyTypes(context.ReferenceData<KeyType>().Read());
    }
  }
}