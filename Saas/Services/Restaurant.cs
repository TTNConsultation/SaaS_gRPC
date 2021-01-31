using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

using Grpc.Core;

using Saas.gRPC;
using Saas.Message.Administrator;

using DbContext.Interface;
using Google.Protobuf.WellKnownTypes;

namespace Saas.Services
{
  [Authorize]
  internal class RestaurantService : RestaurantSvc.RestaurantSvcBase
  {
    private readonly ILogger<RestaurantService> _logger;
    private readonly IDbContext _dbContext;
    private readonly App _app;

    public RestaurantService(ILogger<RestaurantService> log, IDbContext dbContext, App app)
    {
      _logger = log;
      _dbContext = dbContext;
      _app = app;
    }

    public override async Task<Restaurant> Get(Value id, ServerCallContext context)
    {
      using var sp = _dbContext.Read<Restaurant>(_app.RefDatas.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return await (sp.IsReady ? Task.FromResult(sp.Read((int)id.NumberValue))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error))).ConfigureAwait(false);
    }

    public override async Task<Restaurants> Lookup(Value lookupStr, ServerCallContext context)
    {
      //var ctx = context.GetHttpContext().Connection.ClientCertificate;

      using var sp = _dbContext.Read<Restaurant>(_app.RefDatas.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return await (sp.IsReady ? Task.FromResult(new Restaurants(sp.Read(lookupStr.StringValue)))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error))).ConfigureAwait(false);
    }

    [Authorize(Policy = "admin")]
    public override async Task<Value> Create(Restaurant obj, ServerCallContext context)
    {
      using var sp = _dbContext.Write<Restaurant>(_app.RefDatas.AppSetting.Id, context.GetHttpContext().User, OperationType.C);
      return await (sp.IsReady ? Task.FromResult(new Value() { NumberValue = sp.Create(obj) })
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error))).ConfigureAwait(false);
    }

    [Authorize(Policy = "admin")]
    public override async Task<Value> Update(Restaurant obj, ServerCallContext context)
    {
      using var sp = _dbContext.Write<Restaurant>(_app.RefDatas.AppSetting.Id, context.GetHttpContext().User, OperationType.U);
      return await (sp.IsReady ? Task.FromResult(new Value { BoolValue = sp.Update(obj) })
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error))).ConfigureAwait(false);
    }

    [Authorize(Policy = "admin")]
    public override async Task<Value> Delete(Value id, ServerCallContext context)
    {
      using var sp = _dbContext.Write<Restaurant>(_app.RefDatas.AppSetting.Id, context.GetHttpContext().User, OperationType.D);
      return await (sp.IsReady ? Task.FromResult(new Value { BoolValue = sp.UpdateState((int)id.NumberValue, _app.RefDatas.States.DeleteId) })
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error))).ConfigureAwait(false);
    }
  }
}