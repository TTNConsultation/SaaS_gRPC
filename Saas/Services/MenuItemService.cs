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
    private readonly IContext DbContext;
    private readonly ReferenceData RefData;

    public MenuItemService(ILogger<MenuItemService> log, IContext sp, ReferenceData refdata)
    {
      logger = log;
      DbContext = sp;
      RefData = refdata;
    }

    public override Task<MenuItem> Get(MsgInt id, ServerCallContext context)
    {
      using var sp = DbContext.ReadOnly<MenuItem>(RefData.App.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady()) ? Task.FromResult(sp.Read(id.Value))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.ErrorMessages()));
    }

    public async override Task<MenuItem> GetByMenuAndItem(MenuItemIds menuItemIds, ServerCallContext context)
    {
      var parameters = new Dictionary<string, object>
      {
        { typeof(Menu).Name.Id(), menuItemIds.MenuId },
        { typeof(Item).Name.Id(), menuItemIds.ItemId }
      };

      using var sp = DbContext.ReadOnly<MenuItem>(RefData.App.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady()) ? await Task.FromResult(sp.ReadAsync(parameters).Result.First()).ConfigureAwait(false)
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.ErrorMessages()));
    }

    public override Task<MenuItems> GetByItem(MsgInt itemId, ServerCallContext context)
    {
      using var sp = DbContext.ReadOnly<MenuItem>(RefData.App.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady()) ? Task.FromResult(new MenuItems(sp.Read(typeof(Item).Name.Id(), itemId.Value)))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.ErrorMessages()));
    }

    public override Task<MenuItems> GetByMenu(MsgInt menuId, ServerCallContext context)
    {
      using var sp = DbContext.ReadOnly<MenuItem>(RefData.App.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady()) ? Task.FromResult(new MenuItems(sp.Read(typeof(Menu).Name.Id(), menuId.Value)))
                            : throw new RpcException(new Status(StatusCode.InvalidArgument, sp.ErrorMessages()));
    }

    public override Task<MsgInt> Create(MenuItem obj, ServerCallContext context)
    {
      using var sp = DbContext.ReadWrite<MenuItem>(RefData.App.Id, context.GetHttpContext().User, OperationType.C);
      return (sp.IsReady()) ? Task.FromResult(new MsgInt(sp.Create(obj)))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.ErrorMessages()));
    }
  }
}