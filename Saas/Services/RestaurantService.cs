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
      if (id is null || id.Value <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var sp = spContext.ReadOnly<Restaurant>(refData.AppId, context.GetHttpContext().User, OperationType.R);
      return (sp.IsInit()) ? Task.FromResult(sp.Read(id.Value))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }

    public async override Task<Restaurants> Lookup(MsgString lookupStr, ServerCallContext context)
    {
      if (lookupStr is null || string.IsNullOrEmpty(lookupStr.Value))
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var sp = spContext.ReadOnly<Restaurant>(refData.AppId, context.GetHttpContext().User, OperationType.R);
      return (sp.IsInit()) ? new Restaurants(await sp.ReadAsync(lookupStr.Value).ConfigureAwait(false))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }

    [Authorize(Policy = "admin")]
    public override Task<MsgInt> Create(Restaurant obj, ServerCallContext context)
    {
      if (obj is null)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var sp = spContext.ReadWrite<Restaurant>(refData.AppId, context.GetHttpContext().User, OperationType.C);
      return (sp.IsInit()) ? Task.FromResult(new MsgInt(sp.Create(obj)))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }

    [Authorize(Policy = "admin")]
    public override Task<MsgBool> Update(Restaurant obj, ServerCallContext context)
    {
      if (obj is null)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var sp = spContext.ReadWrite<Restaurant>(refData.AppId, context.GetHttpContext().User, OperationType.U);
      return (sp.IsInit()) ? Task.FromResult(new MsgBool(sp.Update(obj)))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }

    [Authorize(Policy = "admin")]
    public override Task<MsgBool> Delete(MsgInt id, ServerCallContext context)
    {
      if (id is null || id.Value <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var sp = spContext.ReadWrite<Restaurant>(refData.AppId, context.GetHttpContext().User, OperationType.D);
      return (sp.IsInit()) ? Task.FromResult(new MsgBool(sp.UpdateState(id.Value, refData.States.DeleteId)))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }
  }
}