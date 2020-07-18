using IdentityServer4.Services;
using System.Threading.Tasks;

namespace IdentityServer
{
  public class CorsPolicyService : ICorsPolicyService
  {
    public Task<bool> IsOriginAllowedAsync(string origin)
    {
      return Task.FromResult(true);
    }
  }
}