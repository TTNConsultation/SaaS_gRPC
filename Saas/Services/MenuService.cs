using System.Threading.Tasks;

using Grpc.Core;

using Microsoft.Extensions.Logging;

using Saas.gRPC;
using Saas.Entity.Administrator;
using Saas.Entity.Common;
using Saas.Entity.App;

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
    private readonly ReferenceData refData;

    public MenuService(ILogger<RestaurantMenuService> log, IContext sp, ReferenceData refdata)
    {
      logger = log;
      spContext = sp;
      refData = refdata;
    }

    public override Task<Menu> Get(MsgInt id, ServerCallContext context)
    {
      if (id is null || id.Value <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var sp = spContext.ReadOnly<Menu>(refData.AppId, context.GetHttpContext().User, OperationType.R);
      return (sp.IsInit()) ? Task.FromResult(sp.Read(id.Value))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }

    public async override Task<Menus> GetByRestaurantMenu(MsgInt restaurantMenuId, ServerCallContext context)
    {
      if (restaurantMenuId is null || restaurantMenuId.Value <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var sp = spContext.ReadOnly<Menu>(refData.AppId, context.GetHttpContext().User, OperationType.R);
      return (sp.IsInit()) ? await Task.FromResult(new Menus(sp.ReadAsync(typeof(RestaurantMenu).Name.Id(), restaurantMenuId.Value).Result)).ConfigureAwait(false)
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }

    public async override Task<Menus> GetByRestaurant(MsgInt restaurantId, ServerCallContext context)
    {
      if (restaurantId is null || restaurantId.Value <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var sp = spContext.ReadOnly<Menu>(refData.AppId, context.GetHttpContext().User, OperationType.R);
      return (sp.IsInit()) ? await Task.FromResult(new Menus(sp.ReadAsync(typeof(Restaurant).Name.Id(), restaurantId.Value).Result)).ConfigureAwait(false)
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }

    public override Task<MsgInt> Create(Menu obj, ServerCallContext context)
    {
      if (obj is null)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var sp = spContext.ReadWrite<Menu>(refData.AppId, context.GetHttpContext().User, OperationType.C);
      return (sp.IsInit()) ? Task.FromResult(new MsgInt(sp.Create(obj)))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }
  }
}