﻿using System;
using System.Threading.Tasks;

using Grpc.Core;

using Microsoft.Extensions.Logging;

using Saas.gRPC;
using Saas.Entity.Administrator;
using Saas.Entity.Common;
using Saas.Entity.App;

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
    private readonly ReferenceData refData;

    public TableService(ILogger<TableService> log, IContext sp, ReferenceData refdata)
    {
      logger = log;
      spContext = sp;
      refData = refdata;
    }

    public override Task<Table> Get(MsgInt id, ServerCallContext context)
    {
      using var sp = spContext.ReadOnly<Table>(refData.App.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady()) ? Task.FromResult(sp.Read(id.Value))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.ErrorMessages()));
    }

    public override Task<Tables> GetByRestaurant(MsgInt restaurantId, ServerCallContext context)
    {
      using var sp = spContext.ReadOnly<Table>(refData.App.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady()) ? Task.FromResult(new Tables(sp.Read(typeof(Restaurant).Name.Id(), restaurantId.Value)))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.ErrorMessages()));
    }
  }
}