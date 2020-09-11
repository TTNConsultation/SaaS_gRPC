using System.Linq;

using System.Threading.Tasks;
using System.Collections.Generic;

using Grpc.Core;

using Microsoft.Extensions.Logging;

using Saas.gRPC;
using Saas.Message.Administrator;
using Saas.Message.Common;
using Saas.Message.Reference;
using Dal.Sp;

using static Saas.Message.Administrator.MenuItems.Types;
using static Saas.Message.Administrator.Menus.Types;
using static Saas.Message.Administrator.Items.Types;

namespace Saas.Services
{
  internal class MenuItemService : MenuItemSvc.MenuItemSvcBase
  {
    private readonly ILogger<MenuItemService> logger;
    private readonly IDbContext DbContext;
    private readonly References RefData;

    public MenuItemService(ILogger<MenuItemService> log, IDbContext sp, App appData)
    {
      logger = log;
      DbContext = sp;
      RefData = appData.RefDatas;
    }

    public override Task<MenuItem> Get(MsgInt id, ServerCallContext context)
    {
      using var sp = DbContext.ReadContext<MenuItem>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? Task.FromResult(sp.Read(id.Value))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public async override Task<MenuItem> GetByMenuAndItem(MenuItemIds menuItemIds, ServerCallContext context)
    {
      var parameters = new Dictionary<string, object>
      {
        { typeof(Menu).Name.Id(), menuItemIds.MenuId },
        { typeof(Item).Name.Id(), menuItemIds.ItemId }
      };

      using var sp = DbContext.ReadContext<MenuItem>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? await Task.FromResult(sp.ReadAsync(parameters).Result.First()).ConfigureAwait(false)
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override Task<MenuItems> GetByItem(MsgInt itemId, ServerCallContext context)
    {
      using var sp = DbContext.ReadContext<MenuItem>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? Task.FromResult(new MenuItems(sp.ReadBy<Item>(itemId.Value)))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override Task<MenuItems> GetByMenu(MsgInt menuId, ServerCallContext context)
    {
      using var sp = DbContext.ReadContext<MenuItem>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? Task.FromResult(new MenuItems(sp.ReadBy<Menu>(menuId.Value)))
                            : throw new RpcException(new Status(StatusCode.InvalidArgument, sp.Error));
    }

    public override Task<MsgInt> Create(MenuItem obj, ServerCallContext context)
    {
      using var sp = DbContext.WriteContext<MenuItem>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.C);
      return (sp.IsReady) ? Task.FromResult(new MsgInt(sp.Create(obj)))
                            : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }
  }
}