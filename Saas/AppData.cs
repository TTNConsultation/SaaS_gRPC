using System;
using System.Linq;
using Saas.Entity.App;
using Dal.Sp;

using static Saas.Entity.App.Languages.Types;
using static Saas.Entity.App.KeyTypes.Types;
using static Saas.Entity.App.States.Types;

namespace Saas
{
  public sealed class IAppData : Dal.Sp.IAppData
  {
    public readonly States States;
    public readonly Languages Languages;
    public readonly KeyTypes KeyTypes;
    public readonly AppSetting AppInfo;

    public IAppData(IContext spContext)
    {
      if (spContext == null)
        throw new NotSupportedException();

      States = new States(spContext.SpAppData<State>().ReadAsync().Result);
      Languages = new Languages(spContext.SpAppData<Language>().ReadAsync().Result);
      KeyTypes = new KeyTypes(spContext.SpAppData<KeyType>().ReadAsync().Result);
      AppInfo = spContext.SpAppData<AppSetting>()?.ReadAsync()?.Result?.First() ?? new AppSetting();
    }

    public int AppId() => AppInfo.Id;
  }
}