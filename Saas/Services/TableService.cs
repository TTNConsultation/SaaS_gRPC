using Saas.gRPC;
using Grpc.Core;

using Saas.Entity;

using Saas.Entity.Administrator;
using Saas.Entity.Common;
using Saas.Entity.ReferenceData;

using Dal.Sp;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

using static Saas.Entity.Administrator.Tables.Types;
using static Saas.Entity.Administrator.Restaurants.Types;

namespace Saas.Services
{
  internal class TableService : TableSvc.TableSvcBase
  {
    private readonly ILogger<TableService> logger;
    private readonly IContext DbContext;
    private readonly ReferenceDatas RefData;

    public TableService(ILogger<TableService> log, IContext sp, AppData appData)
    {
      logger = log;
      DbContext = sp;
      RefData = appData.RefDatas;
    }

    public override Task<Table> Get(MsgInt id, ServerCallContext context)
    {
      using var sp = DbContext.ReadOnly<Table>(RefData.App.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady()) ? Task.FromResult(sp.Read(id.Value))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error()));
    }

    public override Task<Tables> GetByRestaurant(MsgInt restaurantId, ServerCallContext context)
    {
      using var sp = DbContext.ReadOnly<Table>(RefData.App.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady()) ? Task.FromResult(new Tables(sp.Read(typeof(Restaurant).Name.Id(), restaurantId.Value)))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error()));
    }
  }
}