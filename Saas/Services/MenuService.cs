using System.Threading.Tasks;

using Grpc.Core;

using Microsoft.Extensions.Logging;

using Saas.gRPC;
using Saas.Entity;
using Saas.Entity.Administrator;
using Saas.Entity.Common;
using Saas.Entity.Reference;

using Dal.Sp;

using static Saas.Entity.Administrator.Menus.Types;
using static Saas.Entity.Administrator.RestaurantMenus.Types;
using static Saas.Entity.Administrator.Restaurants.Types;
using static Saas.Entity.Language.SupportedLanguages.Types;

namespace Saas.Services
{
  internal class MenuService : MenuSvc.MenuSvcBase
  {
    private readonly ILogger<RestaurantMenuService> logger;
    private readonly IDbContext DbContext;
    private readonly References RefData;

    public MenuService(ILogger<RestaurantMenuService> log, IDbContext sp, App appData)
    {
      logger = log;
      DbContext = sp;
      RefData = appData.RefDatas;
    }

    public override Task<Menu> Get(MsgInt id, ServerCallContext context)
    {
      using var sp = DbContext.ReadOnly<Menu>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsNotNull()) ? Task.FromResult(sp.Read(id.Value))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error()));
    }

    public async override Task<Menus> GetByRestaurantMenu(MsgInt restaurantMenuId, ServerCallContext context)
    {
      using var sp = DbContext.ReadOnly<Menu>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsNotNull()) ? await Task.FromResult(new Menus(sp.ReadAsyncBy<RestaurantMenu>(restaurantMenuId.Value).Result)).ConfigureAwait(false)
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error()));
    }

    public async override Task<Menus> GetByRestaurant(MsgInt restaurantId, ServerCallContext context)
    {
      using var sp = DbContext.ReadOnly<Menu>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsNotNull()) ? await Task.FromResult(new Menus(sp.ReadAsyncBy<Restaurant>(restaurantId.Value).Result)).ConfigureAwait(false)
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error()));
    }

    public override Task<MsgInt> Create(Menu obj, ServerCallContext context)
    {
      using var sp = DbContext.Write<Menu>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.C);
      return (sp.IsNotNull()) ? Task.FromResult(new MsgInt(sp.Create(obj)))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error()));
    }
  }
}