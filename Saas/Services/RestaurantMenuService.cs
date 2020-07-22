using Saas.Entity.Administrator;
using Saas.Entity.Common;
using Saas.Entity.Reference;
using Saas.Entity;

using Saas.gRPC;
using Grpc.Core;

using Dal.Sp;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using static Saas.Entity.Administrator.RestaurantMenus.Types;
using static Saas.Entity.Administrator.Restaurants.Types;

namespace Saas.Services
{
  internal class RestaurantMenuService : RestaurantMenuSvc.RestaurantMenuSvcBase
  {
    private readonly ILogger<RestaurantMenuService> logger;
    private readonly IDbContext DbContext;
    private readonly References RefData;

    public RestaurantMenuService(ILogger<RestaurantMenuService> log, IDbContext sp, App appData)
    {
      logger = log;
      DbContext = sp;
      RefData = appData.RefDatas;
    }

    public override Task<RestaurantMenu> Get(MsgInt id, ServerCallContext context)
    {
      using var sp = DbContext.ReadOnly<RestaurantMenu>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsNotNull()) ? Task.FromResult(sp.Read(id.Value))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error()));
    }

    public async override Task<RestaurantMenus> GetByRestaurant(MsgInt restaurantId, ServerCallContext context)
    {
      using var sp = DbContext.ReadOnly<RestaurantMenu>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsNotNull()) ? await Task.FromResult(new RestaurantMenus(sp.ReadAsync(typeof(Restaurant).Name.Id(), restaurantId.Value).Result)).ConfigureAwait(false)
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error()));
    }

    public override Task<MsgInt> Create(RestaurantMenu obj, ServerCallContext context)
    {
      using var sp = DbContext.Write<RestaurantMenu>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.C);
      return (sp.IsNotNull()) ? Task.FromResult(new MsgInt(sp.Create(obj)))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error()));
    }
  }
}