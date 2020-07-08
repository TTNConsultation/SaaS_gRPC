using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;

using Grpc.Core;

using Dal;
using Dal.Sp;
using Saas.gRPC;
using Saas.Entity.Administrator;
using Saas.Entity.Common;

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
    private readonly IContext spContext;
    private readonly IAppData appData;

    public ItemService(ILogger<ItemService> log, IContext sp, IAppData app)
    {
      logger = log;
      spContext = sp;
      appData = app;
    }

    public async override Task<Item> Get(MsgInt id, ServerCallContext context)
    {
      if (id == null || id.Value <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var db = spContext.SpROnly<Item>(appData, context.GetHttpContext().User, OperationType.R);
      return (db == null) ? throw new RpcException(new Status(StatusCode.PermissionDenied, ""))
                          : await db.ReadAsync(id.Value).ConfigureAwait(false);
    }

    public async override Task<Items> GetByRestaurant(MsgInt restaurantId, ServerCallContext context)
    {
      if (restaurantId == null || restaurantId.Value <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var db = spContext.SpROnly<Item>(appData, context.GetHttpContext().User, OperationType.R);
      return (db == null) ? throw new RpcException(new Status(StatusCode.PermissionDenied, ""))
                          : new Items(await db.ReadAsync(typeof(Restaurant).Name.Id(), restaurantId.Value).ConfigureAwait(false));
    }

    public async override Task<Items> GetByRestaurantMenu(MsgInt restaurantMenuId, ServerCallContext context)
    {
      if (restaurantMenuId == null || restaurantMenuId.Value <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var dbMenu = spContext.SpROnly<Menu>(appData, context.GetHttpContext().User, OperationType.R);
      var menuIds = dbMenu?.ReadAsync(typeof(RestaurantMenu).Name.Id(), restaurantMenuId.Value)?.Result.Select(m => m.Id).Distinct();

      if (menuIds == null)
        return new Items();

      using var dbMenuItem = spContext.SpROnly<MenuItem>(appData, context.GetHttpContext().User, OperationType.R);
      var itemIds = dbMenuItem?.ReadRangeAsync(typeof(Menu).Name.Id(), string.Join(Constant.COMA, menuIds), ',')?.Result.Select(mi => mi.ItemId).Distinct();

      if (itemIds == null)
        return new Items();

      using var dbItem = spContext.SpROnly<Item>(appData, context.GetHttpContext().User, OperationType.R);
      return (dbItem == null) ? throw new RpcException(new Status(StatusCode.PermissionDenied, ""))
                          : await Task.FromResult(new Items(dbItem.ReadRangeAsync(Constant.ID, string.Join(Constant.COMA, itemIds), ',').Result)).ConfigureAwait(false);
    }

    public override Task<MsgBool> Update(Item obj, ServerCallContext context)
    {
      if (obj == null)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var db = spContext.SpCrud<Item>(appData, context.GetHttpContext().User, OperationType.C);
      return (db == null) ? throw new RpcException(new Status(StatusCode.PermissionDenied, ""))
                          : Task.FromResult(new MsgBool(db.Update(obj)));
    }
  }
}