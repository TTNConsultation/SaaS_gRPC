using Grpc.Core;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Saas.gRPC;
using Saas.Entity.Administrator;
using Saas.Entity.Common;
using Dal;
using Dal.Sp;

using static Saas.Entity.Administrator.RestaurantMenus.Types;
using static Saas.Entity.Administrator.Restaurants.Types;

namespace Saas.Services
{
  internal class RestaurantMenuService : RestaurantMenuSvc.RestaurantMenuSvcBase
  {
    private readonly ILogger<RestaurantMenuService> logger;
    private readonly IContext spContext;
    private readonly IAppData appData;

    public RestaurantMenuService(ILogger<RestaurantMenuService> log, IContext db, IAppData app)
    {
      logger = log;
      spContext = db;
      appData = app;
    }

    public async override Task<RestaurantMenu> Get(MsgInt id, ServerCallContext context)
    {
      if (id is null || id.Value <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var db = spContext.SpROnly<RestaurantMenu>(appData, context.GetHttpContext().User, OperationType.R);
      return await db.ReadAsync(id.Value).ConfigureAwait(false) ?? throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }

    public async override Task<RestaurantMenus> GetByRestaurant(MsgInt restaurantId, ServerCallContext context)
    {
      if (restaurantId is null || restaurantId.Value <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var db = spContext.SpROnly<RestaurantMenu>(appData, context.GetHttpContext().User, OperationType.R);
      return await Task.FromResult(new RestaurantMenus(db.ReadAsync(typeof(Restaurant).Name.Id(), restaurantId.Value).Result)).ConfigureAwait(false) ??
             throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }

    public override Task<MsgInt> Create(RestaurantMenu obj, ServerCallContext context)
    {
      if (obj is null)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var db = spContext.SpCrud<RestaurantMenu>(appData, context.GetHttpContext().User, OperationType.C);
      return Task.FromResult(new MsgInt(db.Create(obj))) ?? throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }
  }
}