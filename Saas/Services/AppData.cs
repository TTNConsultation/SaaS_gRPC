using Grpc.Core;
using Microsoft.Extensions.Logging;
using Saas.gRPC;
using Saas.Message.Common;
using Saas.Message.Language;
using Saas.Message.Reference;
using System.Threading.Tasks;


using StoreProcedure.Interface;

namespace Saas.Services
{
  internal class AppData : AppDataSvc.AppDataSvcBase
  {
    private readonly ILogger<RestaurantService> _logger;
    private readonly References _refData;
    private readonly DictionaryCache _dictionnaryCache;
    private readonly IDbContext _dbContext;

    public AppData(ILogger<RestaurantService> log, App appData, IDbContext context)
    {
      _logger = log;
      _refData = appData.RefDatas;
      _dictionnaryCache = appData.DictCache;
      _dbContext = context;
    }

    public override Task<SupportedLanguages> SupportedLanguages(MsgEmpty request, ServerCallContext context)
    {
      return Task.FromResult(_refData.Languages);
    }

    public override Task<States> States(MsgEmpty request, ServerCallContext context)
    {
      return Task.FromResult(_refData.States);
    }

    public override Task<KeyTypes> KeyTypes(MsgEmpty request, ServerCallContext context)
    {
      return Task.FromResult(_refData.KeyTypes);
    }

    public override Task<References> References(MsgEmpty request, ServerCallContext context)
    {
      return Task.FromResult(_refData);
    }

    public override async Task<Dictionary> Dictionary(CodeLanguage lang, ServerCallContext context)
    {
      using var sp = _dbContext.Read<DictKeyValuePair>(_refData.AppSetting.Id, context.GetHttpContext().User, OperationType.R);

      return await Task.FromResult(_dictionnaryCache.Get(sp.RootId, lang) ??
                                   _dictionnaryCache.Add(new Dictionary(sp.RootId,
                                                                lang,
                                                                await _dictionnaryCache.GetKeys(sp.RootId, _dbContext).ConfigureAwait(false),
                                                                await sp.ReadAsync(lang.Code).ConfigureAwait(false)))
                                                               ).ConfigureAwait(false);
    }
  }
}