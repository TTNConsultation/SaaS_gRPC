using System;
using System.Threading.Tasks;

using Grpc.Core;

using Microsoft.Extensions.Logging;

using Saas.gRPC;
using Saas.Entity.Administrator;
using Saas.Entity.Common;
using Dal;
using Dal.Sp;

using static Saas.Entity.Administrator.Menus.Types;
using static Saas.Entity.Administrator.RestaurantMenus.Types;
using static Saas.Entity.Administrator.Restaurants.Types;

namespace Saas.Services
{
  internal class MenuService : MenuSvc.MenuSvcBase
  {
    private readonly ILogger<RestaurantMenuService> logger;
    private readonly IContext spContext;
    private readonly IAppData appData;

    public MenuService(ILogger<RestaurantMenuService> log, IContext sp, IAppData app)
    {
      logger = log;
      spContext = sp;
      appData = app;
    }

    public async override Task<Menu> Get(MsgInt id, ServerCallContext context)
    {
      if (id is null || id.Value <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var db = spContext.SpROnly<Menu>(appData, context.GetHttpContext().User, OperationType.R);
      return (db == null) ? throw new RpcException(new Status(StatusCode.PermissionDenied, ""))
                          : await db.ReadAsync(id.Value).ConfigureAwait(false);
    }

    public async override Task<Menus> GetByRestaurantMenu(MsgInt restaurantMenuId, ServerCallContext context)
    {
      if (restaurantMenuId is null || restaurantMenuId.Value <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var db = spContext.SpROnly<Menu>(appData, context.GetHttpContext().User, OperationType.R);
      return (db == null) ? throw new RpcException(new Status(StatusCode.PermissionDenied, ""))
                          : await Task.FromResult(new Menus(db.ReadAsync(typeof(RestaurantMenu).Name.Id(), restaurantMenuId.Value).Result)).ConfigureAwait(false);
    }

    public async override Task<Menus> GetByRestaurant(MsgInt restaurantId, ServerCallContext context)
    {
      if (restaurantId is null || restaurantId.Value <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var db = spContext.SpROnly<Menu>(appData, context.GetHttpContext().User, OperationType.R);
      return (db == null) ? throw new RpcException(new Status(StatusCode.PermissionDenied, ""))
                          : await Task.FromResult(new Menus(db.ReadAsync(typeof(Restaurant).Name.Id(), restaurantId.Value).Result)).ConfigureAwait(false);
    }

    public override Task<MsgInt> Create(Menu obj, ServerCallContext context)
    {
      if (obj is null)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var db = spContext.SpCrud<Menu>(appData, context.GetHttpContext().User, OperationType.C);
      return (db == null) ? throw new RpcException(new Status(StatusCode.PermissionDenied, ""))
                          : Task.FromResult(new MsgInt(db.Create(obj)));
    }
  }
}