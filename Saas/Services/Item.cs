using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;

using Grpc.Core;
using Dal.Sp;

using Saas.gRPC;
using Saas.Message.Administrator;
using Saas.Message.Common;
using Saas.Message.Reference;

using static Saas.Message.Administrator.Items.Types;
using static Saas.Message.Administrator.MenuItems.Types;
using static Saas.Message.Administrator.Menus.Types;
using static Saas.Message.Administrator.Restaurants.Types;
using static Saas.Message.Administrator.RestaurantMenus.Types;

namespace Saas.Services
{
  internal class ItemService : ItemSvc.ItemSvcBase
  {
    private readonly ILogger<ItemService> logger;
    private readonly IDbContext DbContext;
    private readonly References RefData;

    public ItemService(ILogger<ItemService> log, IDbContext sp, App appData)
    {
      logger = log;
      DbContext = sp;
      RefData = appData.RefDatas;
    }

    public override Task<Item> Get(MsgInt id, ServerCallContext context)
    {
      using var sp = DbContext.ReadContext<Item>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);

      return (sp.IsReady) ? Task.FromResult(sp.Read(id.Value))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override Task<Items> GetByRestaurant(MsgInt restaurantId, ServerCallContext context)
    {
      using var sp = DbContext.ReadContext<Item>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);

      return sp.IsReady ? Task.FromResult(new Items(sp.ReadBy<Restaurant>(restaurantId.Value)))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override Task<Items> GetByRestaurantMenu(MsgInt restaurantMenuId, ServerCallContext context)
    {
      using var spMenu = DbContext.ReadContext<Menu>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);

      var menuIds = (spMenu.IsReady) ? spMenu.ReadBy<RestaurantMenu>(restaurantMenuId.Value)?.Select(m => m.Id).Distinct()
                                       : throw new RpcException(new Status(StatusCode.PermissionDenied, spMenu.Error));

      if (menuIds == null)
        return Task.FromResult(new Items());

      using var spMenuItem = DbContext.ReadContext<MenuItem>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      var itemIds = (spMenuItem.IsReady) ? spMenuItem.ReadRange(typeof(Menu).Name.Id(), string.Join(Constant.COMA, menuIds), ',')?.Select(mi => mi.ItemId).Distinct()
                                           : throw new RpcException(new Status(StatusCode.PermissionDenied, spMenuItem.Error));

      if (itemIds == null)
        return Task.FromResult(new Items());

      using var spItem = DbContext.ReadContext<Item>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return spItem.IsReady ? Task.FromResult(new Items(spItem.ReadRange(Constant.ID, string.Join(Constant.COMA, itemIds), ',')))
                              : throw new RpcException(new Status(StatusCode.PermissionDenied, spItem.Error));
    }

    public override Task<MsgBool> Update(Item obj, ServerCallContext context)
    {
      using var sp = DbContext.WriteContext<Item>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.C);
      return (sp.IsReady) ? Task.FromResult(new MsgBool(sp.Update(obj)))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }
  }
}