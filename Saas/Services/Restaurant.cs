using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

using Protos.Message.Administrator;
using DbContext.Interfaces;

namespace Saas.Services
{
  internal class RestaurantService : RestaurantSvc.RestaurantSvcBase
  {
    //private readonly ILogger<RestaurantService> _logger;
    private readonly IDbContext _dbContext;

    private readonly AppData _app;

    public RestaurantService(IDbContext dbContext, AppData app)
    {
      //_logger = log;
      _dbContext = dbContext;
      _app = app;
    }

    public override Task<Restaurant> Get(Value id, ServerCallContext context)
    {
      using var sp = _dbContext.GetReader<Restaurant>(_app.RefDatas.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? Task.FromResult(sp.Read((int)id.NumberValue))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override Task<Restaurants> Lookup(Value lookupStr, ServerCallContext context)
    {
      //var ctx = context.GetHttpContext().Connection.ClientCertificate;

      using var sp = _dbContext.GetReader<Restaurant>(_app.RefDatas.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? Task.FromResult(new Restaurants(sp.Read(lookupStr.StringValue)))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override Task<Value> Create(Restaurant obj, ServerCallContext context)
    {
      using var sp = _dbContext.GetWriter<Restaurant>(_app.RefDatas.AppSetting.Id, context.GetHttpContext().User, OperationType.C);
      return (sp.IsReady) ? Task.FromResult(new Value() { NumberValue = sp.Create(obj) })
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override Task<Value> Update(Restaurant obj, ServerCallContext context)
    {
      using var sp = _dbContext.GetWriter<Restaurant>(_app.RefDatas.AppSetting.Id, context.GetHttpContext().User, OperationType.U);
      return (sp.IsReady) ? Task.FromResult(new Value { BoolValue = sp.Update(obj) })
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override Task<Value> Delete(Value id, ServerCallContext context)
    {
      using var sp = _dbContext.GetWriter<Restaurant>(_app.RefDatas.AppSetting.Id, context.GetHttpContext().User, OperationType.D);
      return (sp.IsReady) ? Task.FromResult(new Value { BoolValue = sp.UpdateState((int)id.NumberValue, _app.RefDatas.States.DeleteId) })
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }
  }
}