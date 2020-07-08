using System;
using System.Linq;
using Saas.Entity.App;

using Dal.Security;
using Dal.Sp;

using static Saas.Entity.App.Languages.Types;
using static Saas.Entity.App.KeyTypes.Types;
using static Saas.Entity.App.States.Types;

namespace Saas
{
  internal sealed class AppData
  {
    public readonly States States;
    public readonly Languages Languages;
    public readonly KeyTypes KeyTypes;
    public readonly AppSetting AppSetting;
    public int AppId => AppSetting.Id;

    public AppData(RoleManager roles, IContext context)
    {
      var user = User.AppUser(roles);

      States = new States(context.ReadOnly<State>(user).Read());
      Languages = new Languages(context.ReadOnly<Language>(user).Read());
      KeyTypes = new KeyTypes(context.ReadOnly<KeyType>(user).Read());
      AppSetting = context.ReadOnly<AppSetting>(user).Read().First();
    }
  }
}