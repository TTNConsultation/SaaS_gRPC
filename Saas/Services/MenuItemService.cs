using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Grpc.Core;

using Microsoft.Extensions.Logging;

using Saas.gRPC;
using Saas.Entity.Administrator;
using Saas.Entity.Common;
using Saas.Entity.App;

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
    private readonly ReferenceData refData;

    public MenuItemService(ILogger<MenuItemService> log, IContext sp, ReferenceData refdata)
    {
      logger = log;
      spContext = sp;
      refData = refdata;
    }

    public override Task<MenuItem> Get(MsgInt id, ServerCallContext context)
    {
      if (id == null || id.Value <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var sp = spContext.ReadOnly<MenuItem>(refData.AppId, context.GetHttpContext().User, OperationType.R);
      return (sp.IsInit()) ? Task.FromResult(sp.Read(id.Value))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
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

      using var sp = spContext.ReadOnly<MenuItem>(refData.AppId, context.GetHttpContext().User, OperationType.R);
      return (sp.IsInit()) ? await Task.FromResult(sp.ReadAsync(parameters).Result.First()).ConfigureAwait(false)
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }

    public override Task<MenuItems> GetByItem(MsgInt itemId, ServerCallContext context)
    {
      if (itemId == null || itemId.Value <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var sp = spContext.ReadOnly<MenuItem>(refData.AppId, context.GetHttpContext().User, OperationType.R);
      return (sp.IsInit()) ? Task.FromResult(new MenuItems(sp.Read(typeof(Item).Name.Id(), itemId.Value)))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }

    public override Task<MenuItems> GetByMenu(MsgInt menuId, ServerCallContext context)
    {
      if (menuId == null || menuId.Value <= 0)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var sp = spContext.ReadOnly<MenuItem>(refData.AppId, context.GetHttpContext().User, OperationType.R);
      return (sp.IsInit()) ? Task.FromResult(new MenuItems(sp.Read(typeof(Menu).Name.Id(), menuId.Value)))
                            : throw new RpcException(new Status(StatusCode.InvalidArgument, ""));
    }

    public override Task<MsgInt> Create(MenuItem obj, ServerCallContext context)
    {
      if (obj == null)
        throw new RpcException(new Status(StatusCode.InvalidArgument, ""));

      using var sp = spContext.ReadWrite<MenuItem>(refData.AppId, context.GetHttpContext().User, OperationType.C);
      return (sp.IsInit()) ? Task.FromResult(new MsgInt(sp.Create(obj)))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, ""));
    }
  }
}