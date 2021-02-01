﻿using Protos.Shared.Interfaces;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Protos.Shared.Message.Administrator;
using Protos.Shared.Message.Reference;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Protos.Shared;

namespace Saas.Services
{
  internal class MenuItemService : MenuItemSvc.MenuItemSvcBase
  {
    //private readonly ILogger<MenuItemService> _logger;
    private readonly IDbContext _dbContext;

    private readonly References _refData;

    public MenuItemService(IDbContext context, Protos.Shared.AppData appData)
    {
      //_logger = log;
      _dbContext = context;
      _refData = appData.RefDatas;
    }

    public override async Task<MenuItem> Get(Value id, ServerCallContext context)
    {
      using var sp = _dbContext.Read<MenuItem>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return await (sp.IsReady ? Task.FromResult(sp.Read((int)id.NumberValue))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error))).ConfigureAwait(false);
    }

    public async override Task<MenuItem> GetByMenuAndItem(MenuItemIds menuItemIds, ServerCallContext context)
    {
      var parameters = new Dictionary<string, object>
      {
        { typeof(Menu).Name.AsId(), menuItemIds.MenuId },
        { typeof(Item).Name.AsId(), menuItemIds.ItemId }
      };

      using var sp = _dbContext.Read<MenuItem>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? await Task.FromResult(sp.ReadAsync(parameters).Result.First()).ConfigureAwait(false)
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override async Task<MenuItems> GetByItem(Value itemId, ServerCallContext context)
    {
      using var sp = _dbContext.Read<MenuItem>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return await ((sp.IsReady) ? Task.FromResult(new MenuItems(sp.ReadBy<Item>((int)itemId.NumberValue)))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error))).ConfigureAwait(false);
    }

    public override async Task<MenuItems> GetByMenu(Value menuId, ServerCallContext context)
    {
      using var sp = _dbContext.Read<MenuItem>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return await (sp.IsReady ? Task.FromResult(new MenuItems(sp.ReadBy<Menu>((int)menuId.NumberValue)))
                          : throw new RpcException(new Status(StatusCode.InvalidArgument, sp.Error))).ConfigureAwait(false);
    }

    public override async Task<Value> Create(MenuItem obj, ServerCallContext context)
    {
      using var sp = _dbContext.Write<MenuItem>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.C);
      return await (sp.IsReady ? Task.FromResult(new Value { NumberValue = sp.Create(obj) })
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error))).ConfigureAwait(false);
    }
  }
}