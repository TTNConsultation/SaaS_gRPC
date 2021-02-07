using DbContext.Interfaces;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Protos.Message.Administrator;
using Protos.Message.Reference;
using System.Threading.Tasks;

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
      return (sp.IsReady) ? Task.FromResult(new Tables(sp.Read("restaurantid", (int)restaurantId.NumberValue)))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }
  }
}