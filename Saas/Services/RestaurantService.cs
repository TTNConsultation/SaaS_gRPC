using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Grpc.Core;

using Saas.gRPC;
using Saas.Entity.Administrator;
using Saas.Entity.Common;
using Saas.Entity.App;

using Dal.Sp;

using static Saas.Entity.Administrator.Restaurants.Types;
using Microsoft.AspNetCore.Authorization;

namespace Saas.Services
{
  [Authorize]
  internal class RestaurantService : RestaurantSvc.RestaurantSvcBase
  {
    private readonly ILogger<RestaurantService> logger;
    private readonly IContext spContext;
    private readonly ReferenceData refData;

    public RestaurantService(ILogger<RestaurantService> log, IContext sp, ReferenceData refdata)
    {
      logger = log;
      spContext = sp;
      refData = refdata;
    }

    public override Task<Restaurant> Get(MsgInt id, ServerCallContext context)
    {
      using var sp = spContext.ReadOnly<Restaurant>(refData.App.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady()) ? Task.FromResult(sp.Read(id.Value))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.ErrorMessages()));
    }

    public async override Task<Restaurants> Lookup(MsgString lookupStr, ServerCallContext context)
    {
      using var sp = spContext.ReadOnly<Restaurant>(refData.App.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady()) ? new Restaurants(await sp.ReadAsync(lookupStr.Value).ConfigureAwait(false))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.ErrorMessages()));
    }

    [Authorize(Policy = "admin")]
    public override Task<MsgInt> Create(Restaurant obj, ServerCallContext context)
    {
      using var sp = spContext.ReadWrite<Restaurant>(refData.App.Id, context.GetHttpContext().User, OperationType.C);
      return (sp.IsReady()) ? Task.FromResult(new MsgInt(sp.Create(obj)))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.ErrorMessages()));
    }

    [Authorize(Policy = "admin")]
    public override Task<MsgBool> Update(Restaurant obj, ServerCallContext context)
    {
      using var sp = spContext.ReadWrite<Restaurant>(refData.App.Id, context.GetHttpContext().User, OperationType.U);
      return (sp.IsReady()) ? Task.FromResult(new MsgBool(sp.Update(obj)))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.ErrorMessages()));
    }

    [Authorize(Policy = "admin")]
    public override Task<MsgBool> Delete(MsgInt id, ServerCallContext context)
    {
      using var sp = spContext.ReadWrite<Restaurant>(refData.App.Id, context.GetHttpContext().User, OperationType.D);
      return (sp.IsReady()) ? Task.FromResult(new MsgBool(sp.UpdateState(id.Value, refData.States.DeleteId)))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.ErrorMessages()));
    }
  }
}