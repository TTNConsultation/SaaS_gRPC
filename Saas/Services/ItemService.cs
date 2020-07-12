using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;

using Grpc.Core;

using Dal;
using Dal.Sp;
using Saas.gRPC;
using Saas.Entity.Administrator;
using Saas.Entity.Common;
using Saas.Entity.App;

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
    private readonly IContext DbContext;
    private readonly ReferenceData RefData;

    public ItemService(ILogger<ItemService> log, IContext sp, ReferenceData refdata)
    {
      logger = log;
      DbContext = sp;
      RefData = refdata;
    }

    public override Task<Item> Get(MsgInt id, ServerCallContext context)
    {
      using var sp = DbContext.ReadOnly<Item>(RefData.App.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady()) ? Task.FromResult(sp.Read(id.Value))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.ErrorMessages()));
    }

    public async override Task<Items> GetByRestaurant(MsgInt restaurantId, ServerCallContext context)
    {
      using var sp = DbContext.ReadOnly<Item>(RefData.App.Id, context.GetHttpContext().User, OperationType.R);
      return new Items((sp.IsReady()) ? await sp.ReadAsync(typeof(Restaurant).Name.Id(), restaurantId.Value).ConfigureAwait(false)
                                      : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.ErrorMessages())));
    }

    public async override Task<Items> GetByRestaurantMenu(MsgInt restaurantMenuId, ServerCallContext context)
    {
      using var spMenu = DbContext.ReadOnly<Menu>(RefData.App.Id, context.GetHttpContext().User, OperationType.R);
      var menuIds = (spMenu.IsReady()) ? spMenu.ReadAsync(typeof(RestaurantMenu).Name.Id(), restaurantMenuId.Value)?.Result.Select(m => m.Id).Distinct()
                                       : throw new RpcException(new Status(StatusCode.PermissionDenied, spMenu.ErrorMessages()));

      if (menuIds == null)
        return new Items();

      using var spMenuItem = DbContext.ReadOnly<MenuItem>(RefData.App.Id, context.GetHttpContext().User, OperationType.R);
      var itemIds = (spMenuItem.IsReady()) ? spMenuItem.ReadRangeAsync(typeof(Menu).Name.Id(), string.Join(Constant.COMA, menuIds), ',')?.Result.Select(mi => mi.ItemId).Distinct()
                                           : throw new RpcException(new Status(StatusCode.PermissionDenied, spMenuItem.ErrorMessages()));

      if (itemIds == null)
        return new Items();

      using var spItem = DbContext.ReadOnly<Item>(RefData.App.Id, context.GetHttpContext().User, OperationType.R);
      return (spItem.IsReady()) ? await Task.FromResult(new Items(spItem.ReadRangeAsync(Constant.ID, string.Join(Constant.COMA, itemIds), ',').Result)).ConfigureAwait(false)
                                : throw new RpcException(new Status(StatusCode.PermissionDenied, spItem.ErrorMessages()));
    }

    public override Task<MsgBool> Update(Item obj, ServerCallContext context)
    {
      using var sp = DbContext.ReadWrite<Item>(RefData.App.Id, context.GetHttpContext().User, OperationType.C);
      return (sp.IsReady()) ? Task.FromResult(new MsgBool(sp.Update(obj)))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.ErrorMessages()));
    }
  }
}