using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

using Constant;
using Protos.Message.Administrator;
using Protos.Message.Reference;
using DbContext.Interfaces;

namespace Saas.Services
{
  internal class ItemService : ItemSvc.ItemSvcBase
  {
    //private readonly ILogger<ItemService> _logger;
    private readonly IDbContext _dbContext;

    private readonly References _refData;

    public ItemService(IDbContext context, AppData appData)
    {
      //_logger = log;
      _dbContext = context;
      _refData = appData.RefDatas;
    }

    public override Task<Item> Get(Value id, ServerCallContext context)
    {
      using var sp = _dbContext.GetReader<Item>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? Task.FromResult(sp.Read((int)id.NumberValue))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override Task<Items> GetByRestaurant(Value restaurantId, ServerCallContext context)
    {
      using var sp = _dbContext.GetReader<Item>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? Task.FromResult(new Items(sp.Read<Restaurant>((int)restaurantId.NumberValue)))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override Task<Items> GetByRestaurantMenu(Value restaurantMenuId, ServerCallContext context)
    {
      using var spMenu = _dbContext.GetReader<Menu>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      var menuIds = (spMenu.IsReady) ? spMenu.Read<RestaurantMenu>((int)restaurantMenuId.NumberValue)?.Select(m => m.Id).Distinct()
                                     : throw new RpcException(new Status(StatusCode.PermissionDenied, spMenu.Error));

      if (menuIds == null)
        return Task.FromResult(new Items());

      using var spMenuItem = _dbContext.GetReader<MenuItem>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      var itemIds = (spMenuItem.IsReady) ? spMenuItem.ReadRange(typeof(Menu).Name.AsId(), string.Join(StrVal.COMA, menuIds), ',')?.Select(mi => mi.ItemId).Distinct()
                                         : throw new RpcException(new Status(StatusCode.PermissionDenied, spMenuItem.Error));

      if (itemIds == null)
        return Task.FromResult(new Items());

      using var spItem = _dbContext.GetReader<Item>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (spItem.IsReady) ? Task.FromResult(new Items(spItem.ReadRange(StrVal.ID, string.Join(StrVal.COMA, itemIds), ',')))
                              : throw new RpcException(new Status(StatusCode.PermissionDenied, spItem.Error));
    }

    public override Task<Value> Update(Item obj, ServerCallContext context)
    {
      using var sp = _dbContext.GetWriter<Item>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.C);
      return (sp.IsReady) ? Task.FromResult(new Value { BoolValue = sp.Update(obj) })
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }
  }
}