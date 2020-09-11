using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

using Grpc.Core;

using Saas.gRPC;
using Saas.Message.Administrator;
using Saas.Message.Common;
using Saas.Message.Reference;

using Dal.Sp;

using static Saas.Message.Administrator.Restaurants.Types;

namespace Saas.Services
{
  [Authorize]
  internal class RestaurantService : RestaurantSvc.RestaurantSvcBase
  {
    private readonly ILogger<RestaurantService> logger;
    private readonly IDbContext DbContext;
    private readonly References RefData;

    public RestaurantService(ILogger<RestaurantService> log, IDbContext sp, App appData)
    {
      logger = log;
      DbContext = sp;
      RefData = appData.RefDatas;
    }

    public override Task<Restaurant> Get(MsgInt id, ServerCallContext context)
    {
      using var sp = DbContext.ReadContext<Restaurant>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);

      return (sp.IsReady) ? Task.FromResult(sp.Read(id.Value))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override async Task<Restaurants> Lookup(MsgString lookupStr, ServerCallContext context)
    {
      //var ctx = context.GetHttpContext().Connection.ClientCertificate;

      using var sp = DbContext.ReadContext<Restaurant>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);

      return (sp.IsReady) ? new Restaurants(await sp.ReadAsync(lookupStr.Value).ConfigureAwait(false))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    [Authorize(Policy = "admin")]
    public override Task<MsgInt> Create(Restaurant obj, ServerCallContext context)
    {
      using var sp = DbContext.WriteContext<Restaurant>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.C);

      return (sp.IsReady) ? Task.FromResult(new MsgInt(sp.Create(obj)))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    [Authorize(Policy = "admin")]
    public override Task<MsgBool> Update(Restaurant obj, ServerCallContext context)
    {
      using var sp = DbContext.WriteContext<Restaurant>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.U);

      return (sp.IsReady) ? Task.FromResult(new MsgBool(sp.Update(obj)))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    [Authorize(Policy = "admin")]
    public override Task<MsgBool> Delete(MsgInt id, ServerCallContext context)
    {
      using var sp = DbContext.WriteContext<Restaurant>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.D);

      return (sp.IsReady) ? Task.FromResult(new MsgBool(sp.UpdateState(id.Value, RefData.States.DeleteId)))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }
  }
}