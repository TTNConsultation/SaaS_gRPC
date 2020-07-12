using Grpc.Core;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Saas.gRPC;
using Saas.Entity.Administrator;
using Saas.Entity.Common;
using Saas.Entity.App;

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
    private readonly ReferenceData refData;

    public RestaurantMenuService(ILogger<RestaurantMenuService> log, IContext sp, ReferenceData refdata)
    {
      logger = log;
      spContext = sp;
      refData = refdata;
    }

    public override Task<RestaurantMenu> Get(MsgInt id, ServerCallContext context)
    {
      using var sp = spContext.ReadOnly<RestaurantMenu>(refData.App.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady()) ? Task.FromResult(sp.Read(id.Value))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.ErrorMessages()));
    }

    public async override Task<RestaurantMenus> GetByRestaurant(MsgInt restaurantId, ServerCallContext context)
    {
      using var sp = spContext.ReadOnly<RestaurantMenu>(refData.App.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady()) ? await Task.FromResult(new RestaurantMenus(sp.ReadAsync(typeof(Restaurant).Name.Id(), restaurantId.Value).Result)).ConfigureAwait(false)
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.ErrorMessages()));
    }

    public override Task<MsgInt> Create(RestaurantMenu obj, ServerCallContext context)
    {
      using var sp = spContext.ReadWrite<RestaurantMenu>(refData.App.Id, context.GetHttpContext().User, OperationType.C);
      return (sp.IsReady()) ? Task.FromResult(new MsgInt(sp.Create(obj)))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.ErrorMessages()));
    }
  }
}