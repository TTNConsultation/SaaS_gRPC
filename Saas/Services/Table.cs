using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

using Constant;
using Protos.Message.Administrator;
using Protos.Message.Reference;
using DbContext.Interfaces;

namespace Saas.Services
{
  internal class TableService : TableSvc.TableSvcBase
  {
    //private readonly ILogger<TableService> _logger;
    private readonly IDbContext _dbContext;

    private readonly References _refData;

    public TableService(IDbContext context, AppData appData)
    {
      //_logger = log;
      _dbContext = context;
      _refData = appData.RefDatas;
    }

    public override Task<Table> Get(Value id, ServerCallContext context)
    {
      using var sp = _dbContext.GetReader<Table>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? Task.FromResult(sp.Read((int)id.NumberValue))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override Task<Tables> GetByRestaurant(Value restaurantId, ServerCallContext context)
    {
      using var sp = _dbContext.GetReader<Table>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? Task.FromResult(new Tables(sp.Read(typeof(Restaurant).Name.AndId(), (int)restaurantId.NumberValue)))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }
  }
}