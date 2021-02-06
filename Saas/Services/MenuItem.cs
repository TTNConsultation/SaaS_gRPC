using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Constant;
using Protos.Message.Administrator;
using Protos.Message.Reference;
using DbContext.Interfaces;

namespace Saas.Services
{
  internal class MenuItemService : MenuItemSvc.MenuItemSvcBase
  {
    //private readonly ILogger<MenuItemService> _logger;
    private readonly IDbContext _dbContext;

    private readonly References _refData;

    public MenuItemService(IDbContext context, AppData appData)
    {
      //_logger = log;
      _dbContext = context;
      _refData = appData.RefDatas;
    }

    public override Task<MenuItem> Get(Value id, ServerCallContext context)
    {
      using var sp = _dbContext.GetReader<MenuItem>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? Task.FromResult(sp.Read((int)id.NumberValue))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override Task<MenuItem> GetByMenuAndItem(MenuItemIds menuItemIds, ServerCallContext context)
    {
      var parameters = new Dictionary<string, object>
      {
        { typeof(Menu).Name.AsId(), menuItemIds.MenuId },
        { typeof(Item).Name.AsId(), menuItemIds.ItemId }
      };

      using var sp = _dbContext.GetReader<MenuItem>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? Task.FromResult(sp.Read(parameters).First())
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override Task<MenuItems> GetByItem(Value itemId, ServerCallContext context)
    {
      using var sp = _dbContext.GetReader<MenuItem>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? Task.FromResult(new MenuItems(sp.Read<Item>((int)itemId.NumberValue)))
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }

    public override Task<MenuItems> GetByMenu(Value menuId, ServerCallContext context)
    {
      using var sp = _dbContext.GetReader<MenuItem>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return (sp.IsReady) ? Task.FromResult(new MenuItems(sp.Read<Menu>((int)menuId.NumberValue)))
                          : throw new RpcException(new Status(StatusCode.InvalidArgument, sp.Error));
    }

    public override Task<Value> Create(MenuItem obj, ServerCallContext context)
    {
      using var sp = _dbContext.GetWriter<MenuItem>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.C);
      return (sp.IsReady) ? Task.FromResult(new Value { NumberValue = sp.Create(obj) })
                          : throw new RpcException(new Status(StatusCode.PermissionDenied, sp.Error));
    }
  }
}