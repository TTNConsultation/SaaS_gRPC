using Microsoft.AspNetCore.Components.Authorization;
using System.Linq;

namespace PhoKhanhHoa
{
  public static class Extension
  {
    public static string GetClaim(this AuthenticationState authState, string claim) =>
      authState.User.Claims.FirstOrDefault(c => c.Type == claim)?.Value;
  }
}