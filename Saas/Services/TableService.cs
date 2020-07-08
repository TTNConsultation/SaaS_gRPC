using System;
using System.Threading.Tasks;

using Grpc.Core;

using Microsoft.Extensions.Logging;

using Saas.gRPC;
using Saas.Entity.Administrator;
using Saas.Entity.Common;
using Dal;
using Dal.Sp;

using static Saas.Entity.Administrator.Tables.Types;
using static Saas.Entity.Administrator.Restaurants.Types;

namespace Saas.Services
{
  internal class TableService : TableSvc.TableSvcBase
  {
    private readonly ILogger<TableService> logger;
    private readonly IContext spContext;
    private readonly IAppData appData;

    public TableService(ILogger<TableService> log, IContext sp, IAppData app)
    {
      logger = log;
      spContext = sp;
      appData = app;
    }

    public async override Task<Table> Get(MsgInt id, ServerCallContext context)
    {
      if (id is null || id.Value <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var db = spContext.SpROnly<Table>(appData, context.GetHttpContext().User, OperationType.R);
      return await db.ReadAsync(id.Value).ConfigureAwait(false) ?? throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }

    public async override Task<Tables> GetByRestaurant(MsgInt restaurantId, ServerCallContext context)
    {
      if (restaurantId is null || restaurantId.Value <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var db = spContext.SpROnly<Table>(appData, context.GetHttpContext().User, OperationType.R);
      return await Task.FromResult(new Tables(db.ReadAsync(typeof(Restaurant).Name.Id(), restaurantId.Value).Result)).ConfigureAwait(false) ??
             throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }
  }
}