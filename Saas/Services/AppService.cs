using Grpc.Core;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using Saas.gRPC;
using Saas.Entity;
using Saas.Entity.Common;
using Saas.Entity.Language;
using Saas.Entity.Reference;
using Dal.Sp;
using static Saas.Entity.Language.SupportedLanguages.Types;
using static Saas.Entity.Language.Dictionary.Types;

namespace Saas.Services
{
  internal class AppService : AppDataSvc.AppDataSvcBase
  {
    private readonly ILogger<RestaurantService> logger;
    private readonly References RefData;
    private readonly DictionaryCache DictCache;
    private readonly IDbContext DbContext;

    public AppService(ILogger<RestaurantService> log, App appData, IDbContext context)
    {
      logger = log;
      RefData = appData.RefDatas;
      DictCache = appData.DictCache;
      DbContext = context;
    }

    public override Task<SupportedLanguages> SupportedLanguages(MsgEmpty request, ServerCallContext context)
    {
      return Task.FromResult(RefData.Languages);
    }

    public override Task<States> States(MsgEmpty request, ServerCallContext context)
    {
      return Task.FromResult(RefData.States);
    }

    public override Task<KeyTypes> KeyTypes(MsgEmpty request, ServerCallContext context)
    {
      return Task.FromResult(RefData.KeyTypes);
    }

    public override Task<References> References(MsgEmpty request, ServerCallContext context)
    {
      return Task.FromResult(RefData);
    }

    public override async Task<Dictionary> Dictionary(CodeLanguage lang, ServerCallContext context)
    {
      using var sp = DbContext.ReadContext<DictKeyValuePair>(RefData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);

      return await Task.FromResult(DictCache.Get(sp.RootId(), lang) ??
                                   DictCache.Add(new Dictionary(sp.RootId(),
                                                                lang,
                                                                await DictCache.GetKeys(sp.RootId(), DbContext).ConfigureAwait(false),
                                                                await sp.ReadAsync(lang.Code).ConfigureAwait(false)))
                                                               ).ConfigureAwait(false);
    }
  }
}