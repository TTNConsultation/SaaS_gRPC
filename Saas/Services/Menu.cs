using Protos.Shared.Interfaces;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Protos.Shared.Message.Administrator;
using Protos.Shared.Message.Reference;
using System.Threading.Tasks;

namespace Saas.Services
{
  internal class MenuService : MenuSvc.MenuSvcBase
  {
    private readonly ILogger<RestaurantMenuService> _logger;
    private readonly IDbContext _dbContext;
    private readonly References _refData;

    public MenuService(ILogger<RestaurantMenuService> log, IDbContext context, Protos.Shared.AppData appData)
    {
      _logger = log;
      _dbContext = context;
      _refData = appData.RefDatas;
    }

    public override async Task<Menu> Get(Value id, ServerCallContext context)
    {
      using var sp = _dbContext.Read<Menu>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return await (sp.IsReady ? Task.FromResult(sp.Read((int)id.NumberValue))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error))).ConfigureAwait(false);
    }

    public async override Task<Menus> GetByRestaurantMenu(Value restaurantMenuId, ServerCallContext context)
    {
      using var sp = _dbContext.Read<Menu>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? await Task.FromResult(new Menus(sp.ReadAsyncBy<RestaurantMenu>((int)restaurantMenuId.NumberValue).Result)).ConfigureAwait(false)
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public async override Task<Menus> GetByRestaurant(Value restaurantId, ServerCallContext context)
    {
      using var sp = _dbContext.Read<Menu>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? await Task.FromResult(new Menus(sp.ReadAsyncBy<Restaurant>((int)restaurantId.NumberValue).Result)).ConfigureAwait(false)
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override async Task<Value> Create(Menu obj, ServerCallContext context)
    {
      using var sp = _dbContext.Write<Menu>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.C);
      return await ((sp.IsReady) ? Task.FromResult(new Value { NumberValue = sp.Create(obj) })
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error))).ConfigureAwait(false);
    }
  }
}