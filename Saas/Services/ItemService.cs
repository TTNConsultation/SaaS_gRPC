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
    private readonly IContext spContext;
    private readonly ReferenceData refData;

    public ItemService(ILogger<ItemService> log, IContext sp, ReferenceData refdata)
    {
      logger = log;
      spContext = sp;
      refData = refdata;
    }

    public override Task<Item> Get(MsgInt id, ServerCallContext context)
    {
      if (id == null || id.Value <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var sp = spContext.ReadOnly<Item>(refData.AppId, context.GetHttpContext().User, OperationType.R);
      return (sp.IsInit()) ? Task.FromResult(sp.Read(id.Value))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }

    public async override Task<Items> GetByRestaurant(MsgInt restaurantId, ServerCallContext context)
    {
      if (restaurantId == null || restaurantId.Value <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var sp = spContext.ReadOnly<Item>(refData.AppId, context.GetHttpContext().User, OperationType.R);
      return new Items((sp.IsInit()) ? await sp.ReadAsync(typeof(Restaurant).Name.Id(), restaurantId.Value).ConfigureAwait(false)
                                      : throw new RpcException(new Status(StatusCode.PermissionDenied, "")));
    }

    public async override Task<Items> GetByRestaurantMenu(MsgInt restaurantMenuId, ServerCallContext context)
    {
      if (restaurantMenuId == null || restaurantMenuId.Value <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var spMenu = spContext.ReadOnly<Menu>(refData.AppId, context.GetHttpContext().User, OperationType.R);
      var menuIds = (spMenu.IsInit()) ? spMenu.ReadAsync(typeof(RestaurantMenu).Name.Id(), restaurantMenuId.Value)?.Result.Select(m => m.Id).Distinct()
                                       : throw new RpcException(new Status(StatusCode.PermissionDenied, ""));

      if (menuIds == null)
        return new Items();

      using var spMenuItem = spContext.ReadOnly<MenuItem>(refData.AppId, context.GetHttpContext().User, OperationType.R);
      var itemIds = (spMenuItem.IsInit()) ? spMenuItem.ReadRangeAsync(typeof(Menu).Name.Id(), string.Join(Constant.COMA, menuIds), ',')?.Result.Select(mi => mi.ItemId).Distinct()
                                           : throw new RpcException(new Status(StatusCode.PermissionDenied, ""));

      if (itemIds == null)
        return new Items();

      using var spItem = spContext.ReadOnly<Item>(refData.AppId, context.GetHttpContext().User, OperationType.R);
      return (spItem.IsInit()) ? await Task.FromResult(new Items(spItem.ReadRangeAsync(Constant.ID, string.Join(Constant.COMA, itemIds), ',').Result)).ConfigureAwait(false)
                                : throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }

    public override Task<MsgBool> Update(Item obj, ServerCallContext context)
    {
      if (obj == null)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var sp = spContext.ReadWrite<Item>(refData.AppId, context.GetHttpContext().User, OperationType.C);
      return (sp.IsInit()) ? Task.FromResult(new MsgBool(sp.Update(obj)))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }
  }
}