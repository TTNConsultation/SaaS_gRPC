using Grpc.Core;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using Saas.gRPC;
using Saas.Entity.Common;
using Saas.Entity.App;

namespace Saas.Services
{
  internal class AppService : AppDataSvc.AppDataSvcBase
  {
    private readonly ILogger<RestaurantService> logger;
    private readonly ReferenceData refData;

    public AppService(ILogger<RestaurantService> log, ReferenceData refdata)
    {
      logger = log;
      refData = refdata;
    }

    public override Task<Languages> Languages(MsgEmpty request, ServerCallContext context)
    {
      return Task.FromResult(refData.Languages);
    }

    public override Task<States> States(MsgEmpty request, ServerCallContext context)
    {
      return Task.FromResult(refData.States);
    }

    public override Task<KeyTypes> KeyTypes(MsgEmpty request, ServerCallContext context)
    {
      return Task.FromResult(refData.KeyTypes);
    }
  }
}