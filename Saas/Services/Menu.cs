using Protos.Shared.Interfaces;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Protos.Shared.Message.Administrator;
using Protos.Shared.Message.Reference;
using System.Threading.Tasks;

using Protos.Shared;

namespace Saas.Services
{
  internal class MenuService : MenuSvc.MenuSvcBase
  {
    //private readonly ILogger<RestaurantMenuService> _logger;
    private readonly IDbContext _dbContext;

    private readonly References _refData;

    public MenuService(IDbContext context, AppData appData)
    {
      //_logger = log;
      _dbContext = context;
      _refData = appData.RefDatas;
    }

    public override Task<Menu> Get(Value id, ServerCallContext context)
    {
      using var sp = _dbContext.Read<Menu>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? Task.FromResult(sp.Read((int)id.NumberValue))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override Task<Menus> GetByRestaurantMenu(Value restaurantMenuId, ServerCallContext context)
    {
      using var sp = _dbContext.Read<Menu>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? Task.FromResult(new Menus(sp.Read<RestaurantMenu>((int)restaurantMenuId.NumberValue)))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override Task<Menus> GetByRestaurant(Value restaurantId, ServerCallContext context)
    {
      using var sp = _dbContext.Read<Menu>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? Task.FromResult(new Menus(sp.Read<Restaurant>((int)restaurantId.NumberValue)))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override Task<Value> Create(Menu obj, ServerCallContext context)
    {
      using var sp = _dbContext.Write<Menu>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.C);
      return (sp.IsReady) ? Task.FromResult(new Value { NumberValue = sp.Create(obj) })
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }
  }
}