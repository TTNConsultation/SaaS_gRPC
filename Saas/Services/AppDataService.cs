using Grpc.Core;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using Saas.gRPC;
using Saas.Entity.Common;
using Saas.Entity.App;

namespace Saas.Services
{
  internal class AppDataService : AppDataSvc.AppDataSvcBase
  {
    private readonly ILogger<RestaurantService> logger;
    private readonly IAppData appData;

    public AppDataService(ILogger<RestaurantService> log, IAppData app)
    {
      logger = log;
      appData = app;
    }

    public override Task<Languages> Languages(MsgEmpty request, ServerCallContext context)
    {
      return Task.FromResult(appData.Languages);
    }

    public override Task<States> States(MsgEmpty request, ServerCallContext context)
    {
      return Task.FromResult(appData.States);
    }

    public override Task<KeyTypes> KeyTypes(MsgEmpty request, ServerCallContext context)
    {
      return Task.FromResult(appData.KeyTypes);
    }
  }
}