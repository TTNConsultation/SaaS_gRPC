﻿using DbContext;
using DbContext.Interface;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Saas.gRPC;
using Protos.Shared.Message.Administrator;
using Protos.Shared.Message.Reference;
using System.Threading.Tasks;

using Protos.Shared;

namespace Saas.Services
{
  internal class TableService : TableSvc.TableSvcBase
  {
    private readonly ILogger<TableService> _logger;
    private readonly IDbContext _dbContext;
    private readonly References _refData;

    public TableService(ILogger<TableService> log, IDbContext context, Protos.Shared.AppData appData)
    {
      _logger = log;
      _dbContext = context;
      _refData = appData.RefDatas;
    }

    public override async Task<Table> Get(Value id, ServerCallContext context)
    {
      using var sp = _dbContext.Read<Table>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return await ((sp.IsReady) ? Task.FromResult(sp.Read((int)id.NumberValue))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error))).ConfigureAwait(false);
    }

    public override async Task<Tables> GetByRestaurant(Value restaurantId, ServerCallContext context)
    {
      using var sp = _dbContext.Read<Table>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return await ((sp.IsReady) ? Task.FromResult(new Tables(sp.Read(typeof(Restaurant).Name.AsId(), (int)restaurantId.NumberValue)))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error))).ConfigureAwait(false);
    }
  }
}