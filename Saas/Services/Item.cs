using DbContext;
using DbContext.Interface;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Saas.gRPC;
using Protos.Shared.Message.Administrator;
using Protos.Shared.Message.Reference;
using System.Linq;
using System.Threading.Tasks;

using Protos.Shared;

namespace Saas.Services
{
  internal class ItemService : ItemSvc.ItemSvcBase
  {
    private readonly ILogger<ItemService> _logger;
    private readonly IDbContext _dbContext;
    private readonly References _refData;

    public ItemService(ILogger<ItemService> log, IDbContext context, Protos.Shared.AppData appData)
    {
      _logger = log;
      _dbContext = context;
      _refData = appData.RefDatas;
    }

    public override async Task<Item> Get(Value id, ServerCallContext context)
    {
      using var sp = _dbContext.Read<Item>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return await (sp.IsReady ? Task.FromResult(sp.Read((int)id.NumberValue))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error))).ConfigureAwait(false);
    }

    public override async Task<Items> GetByRestaurant(Value restaurantId, ServerCallContext context)
    {
      using var sp = _dbContext.Read<Item>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return await (sp.IsReady ? Task.FromResult(new Items(sp.ReadBy<Restaurant>((int)restaurantId.NumberValue)))
                        : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error))).ConfigureAwait(false);
    }

    public override async Task<Items> GetByRestaurantMenu(Value restaurantMenuId, ServerCallContext context)
    {
      using var spMenu = _dbContext.Read<Menu>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      var menuIds = (spMenu.IsReady) ? spMenu.ReadBy<RestaurantMenu>((int)restaurantMenuId.NumberValue)?.Select(m => m.Id).Distinct()
                                     : throw new RpcException(new Status(StatusCode.PermissionDenied, spMenu.Error));

      if (menuIds == null)
        return await Task.FromResult(new Items()).ConfigureAwait(false);

      using var spMenuItem = _dbContext.Read<MenuItem>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      var itemIds = (spMenuItem.IsReady) ? spMenuItem.ReadRange(typeof(Menu).Name.AsId(), string.Join(Constant.COMA, menuIds), ',')?.Select(mi => mi.ItemId).Distinct()
                                         : throw new RpcException(new Status(StatusCode.PermissionDenied, spMenuItem.Error));

      if (itemIds == null)
        return await Task.FromResult(new Items()).ConfigureAwait(false);

      using var spItem = _dbContext.Read<Item>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return await (spItem.IsReady ? Task.FromResult(new Items(spItem.ReadRange(Constant.ID, string.Join(Constant.COMA, itemIds), ',')))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, spItem.Error))).ConfigureAwait(false);
    }

    public override async Task<Value> Update(Item obj, ServerCallContext context)
    {
      using var sp = _dbContext.Write<Item>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.C);
      return await (sp.IsReady ? Task.FromResult(new Value { BoolValue = sp.Update(obj) })
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error))).ConfigureAwait(false);
    }
  }
}