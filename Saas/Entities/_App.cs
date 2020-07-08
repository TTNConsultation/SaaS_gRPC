using System.Collections.Generic;
using System.Globalization;

using System.Linq;

using Dal;
using Dal.Sp;

using static Saas.Entity.App.KeyTypes.Types;
using static Saas.Entity.App.Languages.Types;
using static Saas.Entity.App.States.Types;

namespace Saas.Entity.App
{
  public partial class States
  {
    public int DeleteId => Values.First(s => string.Compare(s.Name, Constant.DELETE, true, CultureInfo.CurrentCulture) == 0).Id;
    public int EnableId => Values.First(s => string.Compare(s.Name, Constant.ENABLE, true, CultureInfo.CurrentCulture) == 0).Id;
    public int DisableId => Values.First(s => string.Compare(s.Name, Constant.DISABLE, true, CultureInfo.CurrentCulture) == 0).Id;

    public States(IEnumerable<Types.State> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class Languages
  {
    public Languages(IEnumerable<Types.Language> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class Keys
  {
    public Keys(IEnumerable<Types.Key> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class French
  {
    public French(IEnumerable<Types.Value> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class English
  {
    public English(IEnumerable<Types.Value> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class Vietnamese
  {
    public Vietnamese(IEnumerable<Types.Value> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class KeyTypes
  {
    public KeyTypes(IEnumerable<Types.KeyType> values)
    {
      Values.AddRange(values);
    }
  }

  internal class ReferenceData
  {
    public int AppId => AppSettings.Id;
    public readonly States States;
    public readonly Languages Languages;
    public readonly KeyTypes KeyTypes;
    public readonly AppSetting AppSettings;

    public ReferenceData(IContext context)
    {
      States = new States(context.ReferenceData<State>().Read());
      Languages = new Languages(context.ReferenceData<Language>().Read());
      KeyTypes = new KeyTypes(context.ReferenceData<KeyType>().Read());
      AppSettings = context.ReferenceData<AppSetting>().Read().First();
    }
  }
}