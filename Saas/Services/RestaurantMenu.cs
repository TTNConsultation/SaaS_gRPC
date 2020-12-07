using Saas.Message.Administrator;
using Saas.Message.Common;
using Saas.Message.Reference;
using Saas.gRPC;
using Grpc.Core;

using Dal;
using Dal.Sp;

using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using static Saas.Message.Administrator.RestaurantMenus.Types;

namespace Saas.Services
{
  internal class RestaurantMenuService : RestaurantMenuSvc.RestaurantMenuSvcBase
  {
    private readonly ILogger<RestaurantMenuService> _logger;
    private readonly IDbContext _dbContext;
    private readonly References _refData;

    public RestaurantMenuService(ILogger<RestaurantMenuService> log, IDbContext sp, App appData)
    {
      _logger = log;
      _dbContext = sp;
      _refData = appData.RefDatas;
    }

    public override Task<RestaurantMenu> Get(MsgInt id, ServerCallContext context)
    {
      using var sp = _dbContext.ReadContext<RestaurantMenu>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? Task.FromResult(sp.Read(id.Value))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public async override Task<RestaurantMenus> GetByRestaurant(MsgInt restaurantId, ServerCallContext context)
    {
      using var sp = _dbContext.ReadContext<RestaurantMenu>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? await Task.FromResult(new RestaurantMenus(sp.ReadAsync(typeof(Restaurants.Types.Restaurant).Name.Id(), restaurantId.Value).Result)).ConfigureAwait(false)
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override Task<MsgInt> Create(RestaurantMenu obj, ServerCallContext context)
    {
      using var sp = _dbContext.WriteContext<RestaurantMenu>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.C);
      return (sp.IsReady) ? Task.FromResult(new MsgInt(sp.Create(obj)))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }
  }
}