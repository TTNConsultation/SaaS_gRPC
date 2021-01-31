using Saas.Message.Administrator;
using Saas.Message.Reference;
using Saas.gRPC;
using Grpc.Core;

using DbContext;
using DbContext.Interface;

using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Google.Protobuf.WellKnownTypes;

namespace Saas.Services
{
  internal class RestaurantMenuService : RestaurantMenuSvc.RestaurantMenuSvcBase
  {
    private readonly ILogger<RestaurantMenuService> _logger;
    private readonly IDbContext _dbContext;
    private readonly References _refData;

    public RestaurantMenuService(ILogger<RestaurantMenuService> log, IDbContext context, App appData)
    {
      _logger = log;
      _dbContext = context;
      _refData = appData.RefDatas;
    }

    public override async Task<RestaurantMenu> Get(Value id, ServerCallContext context)
    {
      using var sp = _dbContext.Read<RestaurantMenu>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return await (sp.IsReady ? Task.FromResult(sp.Read((int)id.NumberValue))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error))).ConfigureAwait(false);
    }

    public async override Task<RestaurantMenus> GetByRestaurant(Value restaurantId, ServerCallContext context)
    {
      using var sp = _dbContext.Read<RestaurantMenu>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? await Task.FromResult(new RestaurantMenus(sp.ReadAsync(typeof(Restaurant).Name.AsId(), (int)restaurantId.NumberValue).Result)).ConfigureAwait(false)
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override async Task<Value> Create(RestaurantMenu obj, ServerCallContext context)
    {
      using var sp = _dbContext.Write<RestaurantMenu>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.C);
      return await (sp.IsReady ? Task.FromResult(new Value { NumberValue = sp.Create(obj) })
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error))).ConfigureAwait(false);
    }
  }
}