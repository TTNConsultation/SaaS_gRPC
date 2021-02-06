using Protos.Shared.Interfaces;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Protos.Shared.Message.Language;
using Protos.Shared.Message.Reference;
using System.Threading.Tasks;

using Protos.Shared;

namespace Saas.Services
{
  internal class AppService : AppDataSvc.AppDataSvcBase
  {
    //private readonly ILogger<RestaurantService> _logger;
    private readonly AppData _app;

    private readonly IDbContext _context;

    public AppService(AppData app, IDbContext context)
    {
      //_logger = log;
      _app = app;
      _context = context;
    }

    public override Task<SupportedLanguages> SupportedLanguages(Value request, ServerCallContext context) =>
      Task.FromResult(_app.RefDatas.Languages);

    public override Task<States> States(Value request, ServerCallContext context) =>
      Task.FromResult(_app.RefDatas.States);

    public override Task<KeyTypes> KeyTypes(Value request, ServerCallContext context) =>
      Task.FromResult(_app.RefDatas.KeyTypes);

    public override Task<References> References(Value request, ServerCallContext context) =>
      Task.FromResult(_app.RefDatas);

    public override async Task<Dictionary> Dictionary(CodeLanguage lang, ServerCallContext context)
    {
      using var sp = _context.Read<DictKeyValuePair>(_app.RefDatas.AppSetting.Id, context.GetHttpContext().User, OperationType.R);
      return await Task.FromResult(_app.DictCache.Get(sp.RootId, lang) ??
                                   _app.DictCache.Add(new Dictionary(sp.RootId,
                                                                     lang,
                                                                     await _app.DictCache.GetKeys(sp.RootId, _context).ConfigureAwait(false),
                                                                     await sp.ReadAsync(lang.Code).ConfigureAwait(false)))
                                                                     ).ConfigureAwait(false);
    }
  }
}