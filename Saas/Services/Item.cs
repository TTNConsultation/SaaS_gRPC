using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;

using Grpc.Core;

using StoreProcedure;
using StoreProcedure.Interface;

using Saas.gRPC;
using Saas.Message.Administrator;
using Saas.Message.Common;
using Saas.Message.Reference;

namespace Saas.Services
{
  internal class ItemService : ItemSvc.ItemSvcBase
  {
    private readonly ILogger<ItemService> _logger;
    private readonly IDbContext _dbContext;
    private readonly References _refData;

    public ItemService(ILogger<ItemService> log, IDbContext context, App appData)
    {
      _logger = log;
      _dbContext = context;
      _refData = appData.RefDatas;
    }

    public override Task<Item> Get(MsgInt id, ServerCallContext context)
    {
      using var sp = _dbContext.Read<Item>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);

      return (sp.IsReady) ? Task.FromResult(sp.Read(id.Value))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override Task<Items> GetByRestaurant(MsgInt restaurantId, ServerCallContext context)
    {
      using var sp = _dbContext.Read<Item>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);

      return sp.IsReady ? Task.FromResult(new Items(sp.ReadBy<Restaurant>(restaurantId.Value)))
                        : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override Task<Items> GetByRestaurantMenu(MsgInt restaurantMenuId, ServerCallContext context)
    {
      using var spMenu = _dbContext.Read<Menu>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);

      var menuIds = (spMenu.IsReady) ? spMenu.ReadBy<RestaurantMenu>(restaurantMenuId.Value)?.Select(m => m.Id).Distinct()
                                     : throw new RpcException(new Status(StatusCode.PermissionDenied, spMenu.Error));

      if (menuIds == null)
        return Task.FromResult(new Items());

      using var spMenuItem = _dbContext.Read<MenuItem>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      var itemIds = (spMenuItem.IsReady) ? spMenuItem.ReadRange(typeof(Menu).Name.AsId(), string.Join(Constant.COMA, menuIds), ',')?.Select(mi => mi.ItemId).Distinct()
                                         : throw new RpcException(new Status(StatusCode.PermissionDenied, spMenuItem.Error));

      if (itemIds == null)
        return Task.FromResult(new Items());

      using var spItem = _dbContext.Read<Item>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return spItem.IsReady ? Task.FromResult(new Items(spItem.ReadRange(Constant.ID, string.Join(Constant.COMA, itemIds), ',')))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, spItem.Error));
    }

    public override Task<MsgBool> Update(Item obj, ServerCallContext context)
    {
      using var sp = _dbContext.Write<Item>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.C);
      return (sp.IsReady) ? Task.FromResult(new MsgBool(sp.Update(obj)))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }
  }
}