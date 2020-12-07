using System.Threading.Tasks;

using Grpc.Core;

using Microsoft.Extensions.Logging;

using Saas.gRPC;
using Saas.Message.Administrator;
using Saas.Message.Common;
using Saas.Message.Reference;

using Dal.Sp;

using static Saas.Message.Administrator.Menus.Types;
using static Saas.Message.Administrator.RestaurantMenus.Types;
using static Saas.Message.Administrator.Restaurants.Types;

namespace Saas.Services
{
  internal class MenuService : MenuSvc.MenuSvcBase
  {
    private readonly ILogger<RestaurantMenuService> _logger;
    private readonly IDbContext _dbContext;
    private readonly References _refData;

    public MenuService(ILogger<RestaurantMenuService> log, IDbContext sp, App appData)
    {
      _logger = log;
      _dbContext = sp;
      _refData = appData.RefDatas;
    }

    public override Task<Menu> Get(MsgInt id, ServerCallContext context)
    {
      using var sp = _dbContext.ReadContext<Menus.Types.Menu>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? Task.FromResult(sp.Read(id.Value))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public async override Task<Menus> GetByRestaurantMenu(MsgInt restaurantMenuId, ServerCallContext context)
    {
      using var sp = _dbContext.ReadContext<Menu>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? await Task.FromResult(new Menus(sp.ReadAsyncBy<RestaurantMenus.Types.RestaurantMenu>(restaurantMenuId.Value).Result)).ConfigureAwait(false)
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public async override Task<Menus> GetByRestaurant(MsgInt restaurantId, ServerCallContext context)
    {
      using var sp = _dbContext.ReadContext<Menu>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? await Task.FromResult(new Menus(sp.ReadAsyncBy<Restaurants.Types.Restaurant>(restaurantId.Value).Result)).ConfigureAwait(false)
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override Task<MsgInt> Create(Menu obj, ServerCallContext context)
    {
      using var sp = _dbContext.WriteContext<Menu>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.C);
      return (sp.IsReady) ? Task.FromResult(new MsgInt(sp.Create(obj)))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }
  }
}