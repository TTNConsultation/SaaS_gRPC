using System.Threading.Tasks;
using Grpc.Core;
using Saas.Entity.Common;
using Saas.gRPC.Test;

namespace Saas.Services
{
  public class TestService : TestSvc.TestSvcBase
  {
    public override Task<MsgString> Get(MsgEmpty request, ServerCallContext context)
    {
      var user = context.GetHttpContext().User;
      return Task.FromResult(new MsgString("test"));
    }
  }
}