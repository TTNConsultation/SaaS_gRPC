using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;

using Grpc.Core;
using Dal.Sp;

using Saas.gRPC;
using Saas.Entity.Administrator;
using Saas.Entity.Common;
using Saas.Entity;
using Saas.Entity.Reference;

using static Saas.Entity.Administrator.Items.Types;
using static Saas.Entity.Administrator.MenuItems.Types;
using static Saas.Entity.Administrator.Menus.Types;
using static Saas.Entity.Administrator.Restaurants.Types;
using static Saas.Entity.Administrator.RestaurantMenus.Types;

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
      using var sp = DbContext.ReadOnly<Item>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsNotNull()) ? Task.FromResult(sp.Read(id.Value))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error()));
    }

    public override Task<Items> GetByRestaurant(MsgInt restaurantId, ServerCallContext context)
    {
      using var sp = DbContext.ReadOnly<Item>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return sp.IsNotNull() ? Task.FromResult(new Items(sp.ReadBy<Restaurant>(restaurantId.Value)))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error()));
    }

    public override Task<Items> GetByRestaurantMenu(MsgInt restaurantMenuId, ServerCallContext context)
    {
      using var spMenu = DbContext.ReadOnly<Menu>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      var menuIds = (spMenu.IsNotNull()) ? spMenu.ReadBy<RestaurantMenu>(restaurantMenuId.Value)?.Select(m => m.Id).Distinct()
                                       : throw new RpcException(new Status(StatusCode.PermissionDenied, spMenu.Error()));

      if (menuIds == null)
        return Task.FromResult(new Items());

      using var spMenuItem = DbContext.ReadOnly<MenuItem>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      var itemIds = (spMenuItem.IsNotNull()) ? spMenuItem.ReadRange(typeof(Menu).Name.Id(), string.Join(Constant.COMA, menuIds), ',')?.Select(mi => mi.ItemId).Distinct()
                                           : throw new RpcException(new Status(StatusCode.PermissionDenied, spMenuItem.Error()));

      if (itemIds == null)
        return Task.FromResult(new Items());

      using var spItem = DbContext.ReadOnly<Item>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return spItem.IsNotNull() ? Task.FromResult(new Items(spItem.ReadRange(Constant.ID, string.Join(Constant.COMA, itemIds), ',')))
                              : throw new RpcException(new Status(StatusCode.PermissionDenied, spItem.Error()));
    }

    public override Task<MsgBool> Update(Item obj, ServerCallContext context)
    {
      using var sp = DbContext.Write<Item>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.C);
      return (sp.IsNotNull()) ? Task.FromResult(new MsgBool(sp.Update(obj)))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error()));
    }
  }
}