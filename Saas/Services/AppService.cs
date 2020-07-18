using Grpc.Core;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using Saas.gRPC;
using Saas.Entity;
using Saas.Entity.Common;
using Saas.Entity.Language;
using Saas.Entity.ReferenceData;
using Dal.Sp;

using static Saas.Entity.ReferenceData.SupportedLanguages.Types;

namespace Saas.Services
{
  internal class AppService : AppDataSvc.AppDataSvcBase
  {
    private readonly ILogger<RestaurantService> logger;
    private readonly ReferenceDatas RefData;
    private readonly DictionaryCache DictCache;
    private readonly IContext DbContext;

    public AppService(ILogger<RestaurantService> log, AppData appData, IContext context)
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

    public override Task<ReferenceDatas> ReferenceData(MsgEmpty request, ServerCallContext context)
    {
      return Task.FromResult(RefData);
    }

    public async override Task<Dictionary> MyDictionary(CodeLanguage lang, ServerCallContext context)
    {
      using var sp = DbContext.ReadOnly<DictKeyValuePair>(RefData.App.Id, context.GetHttpContext().User, OperationType.R);
      var rootId = sp.Claim().RootId;
      Dictionary dict = DictCache.Get(rootId, lang);

      return await (dict == null
        ? Task.FromResult(DictCache.Add(new Dictionary(rootId, lang, await DictCache.GetKeys(rootId, DbContext).ConfigureAwait(false), await sp.ReadAsync(lang.Code).ConfigureAwait(false))))
        : Task.FromResult(dict));
    }
  }
}