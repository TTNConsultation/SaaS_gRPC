using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Grpc.Core;

using Microsoft.Extensions.Logging;

using Saas.gRPC;
using Saas.Entity.Administrator;
using Saas.Entity.Common;
using Dal;
using Dal.Sp;

using static Saas.Entity.Administrator.MenuItems.Types;
using static Saas.Entity.Administrator.Menus.Types;
using static Saas.Entity.Administrator.Items.Types;
using System.Linq;

namespace Saas.Services
{
  internal class MenuItemService : MenuItemSvc.MenuItemSvcBase
  {
    private readonly ILogger<MenuItemService> logger;
    private readonly IContext spContext;
    private readonly IAppData appData;

    public MenuItemService(ILogger<MenuItemService> log, IContext sp, IAppData app)
    {
      logger = log;
      spContext = sp;
      appData = app;
    }

    public async override Task<MenuItem> Get(MsgInt id, ServerCallContext context)
    {
      if (id == null || id.Value <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var db = spContext.SpROnly<MenuItem>(appData, context.GetHttpContext().User, OperationType.R);
      return await db.ReadAsync(id.Value).ConfigureAwait(false) ?? throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }

    public async override Task<MenuItem> GetByMenuAndItem(MenuItemIds menuItemIds, ServerCallContext context)
    {
      if (menuItemIds == null || menuItemIds.MenuId <= 0 || menuItemIds.ItemId <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      var parameters = new Dictionary<string, object>
      {
        { typeof(Menu).Name.Id(), menuItemIds.MenuId },
        { typeof(Item).Name.Id(), menuItemIds.ItemId }
      };

      using var db = spContext.SpROnly<MenuItem>(appData, context.GetHttpContext().User, OperationType.R);
      return await Task.FromResult(db.ReadAsync(parameters).Result.First()).ConfigureAwait(false) ?? throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }

    public async override Task<MenuItems> GetByItem(MsgInt itemId, ServerCallContext context)
    {
      if (itemId == null || itemId.Value <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var db = spContext.SpROnly<MenuItem>(appData, context.GetHttpContext().User, OperationType.R);
      return await Task.FromResult(new MenuItems(db.ReadAsync(typeof(Item).Name.Id(), itemId.Value).Result)).ConfigureAwait(false) ??
             throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }

    public async override Task<MenuItems> GetByMenu(MsgInt menuId, ServerCallContext context)
    {
      if (menuId == null || menuId.Value <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var db = spContext.SpROnly<MenuItem>(appData, context.GetHttpContext().User, OperationType.R);
      return await Task.FromResult(new MenuItems(db.ReadAsync(typeof(Menu).Name.Id(), menuId.Value).Result)).ConfigureAwait(false) ??
             throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }

    public override Task<MsgInt> Create(MenuItem obj, ServerCallContext context)
    {
      if (obj == null)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var db = spContext.SpCrud<MenuItem>(appData, context.GetHttpContext().User, OperationType.C);
      return Task.FromResult(new MsgInt(db.Create(obj))) ?? throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }
  }
}