using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Grpc.Core;

using Saas.gRPC;
using Saas.Entity.Administrator;
using Saas.Entity.Common;
using Dal;
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
    private readonly IAppData appData;

    public RestaurantService(ILogger<RestaurantService> log, IContext sp, IAppData app)
    {
      logger = log;
      spContext = sp;
      appData = app;
    }

    public async override Task<Restaurant> Get(MsgInt id, ServerCallContext context)
    {
      if (id is null || id.Value <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var db = spContext.SpROnly<Restaurant>(appData, context.GetHttpContext().User, OperationType.R);
      return await db.ReadAsync(id.Value).ConfigureAwait(false) ?? throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }

    public async override Task<Restaurants> Lookup(MsgString lookupStr, ServerCallContext context)
    {
      if (lookupStr is null || string.IsNullOrEmpty(lookupStr.Value))
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var db = spContext.SpROnly<Restaurant>(appData, context.GetHttpContext().User, OperationType.R);
      return new Restaurants(await db.ReadAsync(lookupStr.Value).ConfigureAwait(false)) ?? throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }

    [Authorize(Policy = "admin")]
    public override Task<MsgInt> Create(Restaurant obj, ServerCallContext context)
    {
      if (obj is null)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var db = spContext.SpCrud<Restaurant>(appData, context.GetHttpContext().User, OperationType.C);
      return Task.FromResult(new MsgInt(db.Create(obj))) ?? throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }

    [Authorize(Policy = "admin")]
    public override Task<MsgBool> Update(Restaurant obj, ServerCallContext context)
    {
      if (obj is null)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var db = spContext.SpCrud<Restaurant>(appData, context.GetHttpContext().User, OperationType.U);
      return Task.FromResult(new MsgBool(db.Update(obj))) ?? throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }

    [Authorize(Policy = "admin")]
    public async override Task<MsgBool> Delete(MsgInt id, ServerCallContext context)
    {
      if (id is null || id.Value <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var db = spContext.SpCrud<Restaurant>(appData, context.GetHttpContext().User, OperationType.D);
      return new MsgBool(await db.UpdateState(id.Value, appData.States.DeleteId)) ?? throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }
  }
}