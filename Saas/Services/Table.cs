using Saas.gRPC;
using Grpc.Core;
using Saas.Message.Administrator;
using Saas.Message.Common;
using Saas.Message.Reference;

using StoreProcedure;
using StoreProcedure.Interface;

using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Saas.Services
{
  internal class TableService : TableSvc.TableSvcBase
  {
    private readonly ILogger<TableService> _logger;
    private readonly IDbContext _dbContext;
    private readonly References _refData;

    public TableService(ILogger<TableService> log, IDbContext context, App appData)
    {
      _logger = log;
      _dbContext = context;
      _refData = appData.RefDatas;
    }

    public override Task<Table> Get(MsgInt id, ServerCallContext context)
    {
      using var sp = _dbContext.Read<Table>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? Task.FromResult(sp.Read(id.Value))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override Task<Tables> GetByRestaurant(MsgInt restaurantId, ServerCallContext context)
    {
      using var sp = _dbContext.Read<Table>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? Task.FromResult(new Tables(sp.Read(typeof(Restaurant).Name.Id(), restaurantId.Value)))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }
  }
}