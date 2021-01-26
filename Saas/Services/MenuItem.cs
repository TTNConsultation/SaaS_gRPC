using System.Linq;

using System.Threading.Tasks;
using System.Collections.Generic;

using Grpc.Core;

using Microsoft.Extensions.Logging;

using StoreProcedure;
using StoreProcedure.Interface;

using Saas.gRPC;
using Saas.Message.Administrator;
using Saas.Message.Reference;
using Google.Protobuf.WellKnownTypes;

namespace Saas.Services
{
  internal class MenuItemService : MenuItemSvc.MenuItemSvcBase
  {
    private readonly ILogger<MenuItemService> _logger;
    private readonly IDbContext _dbContext;
    private readonly References _refData;

    public MenuItemService(ILogger<MenuItemService> log, IDbContext context, App appData)
    {
      _logger = log;
      _dbContext = context;
      _refData = appData.RefDatas;
    }

    public override Task<MenuItem> Get(Value id, ServerCallContext context)
    {
      using var sp = _dbContext.Read<MenuItem>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? Task.FromResult(sp.Read((int)id.NumberValue))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
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

    public override Task<MenuItems> GetByItem(Value itemId, ServerCallContext context)
    {
      using var sp = _dbContext.Read<MenuItem>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? Task.FromResult(new MenuItems(sp.ReadBy<Item>((int)itemId.NumberValue)))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override Task<MenuItems> GetByMenu(Value menuId, ServerCallContext context)
    {
      using var sp = _dbContext.Read<MenuItem>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? Task.FromResult(new MenuItems(sp.ReadBy<Menu>((int)menuId.NumberValue)))
                          : throw new RpcException(new Status(StatusCode.InvalidArgument, sp.Error));
    }

    public override Task<Value> Create(MenuItem obj, ServerCallContext context)
    {
      using var sp = _dbContext.Write<MenuItem>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.C);
      return (sp.IsReady) ? Task.FromResult(new Value { NumberValue = sp.Create(obj) })
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }
  }
}