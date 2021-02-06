using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

using Constant;
using Protos.Message.Administrator;
using Protos.Message.Reference;
using DbContext.Interfaces;

namespace Saas.Services
{
  internal class RestaurantMenuService : RestaurantMenuSvc.RestaurantMenuSvcBase
  {
    //private readonly ILogger<RestaurantMenuService> _logger;
    private readonly IDbContext _dbContext;

    private readonly References _refData;

    public RestaurantMenuService(IDbContext context, AppData appData)
    {
      //_logger = log;
      _dbContext = context;
      _refData = appData.RefDatas;
    }

    public override Task<RestaurantMenu> Get(Value id, ServerCallContext context)
    {
      using var sp = _dbContext.GetReader<RestaurantMenu>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? Task.FromResult(sp.Read((int)id.NumberValue))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override Task<RestaurantMenus> GetByRestaurant(Value restaurantId, ServerCallContext context)
    {
      using var sp = _dbContext.GetReader<RestaurantMenu>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? Task.FromResult(new RestaurantMenus(sp.Read(typeof(Restaurant).Name.AndId(), (int)restaurantId.NumberValue)))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override Task<Value> Create(RestaurantMenu obj, ServerCallContext context)
    {
      using var sp = _dbContext.GetWriter<RestaurantMenu>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.C);
      return (sp.IsReady) ? Task.FromResult(new Value { NumberValue = sp.Create(obj) })
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }
  }
}