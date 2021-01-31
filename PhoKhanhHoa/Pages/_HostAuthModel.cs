using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace PhoKhanhHoa.Pages
{
  public class _HostAuthModel : PageModel
  {
    public IActionResult OnGet()
    {
      return Page();
    }

    public IActionResult OnGetLogin()
    {
      System.Diagnostics.Debug.WriteLine("\n_Host OnGetLogin");
      var authProps = new AuthenticationProperties
      {
        IsPersistent = true,
        ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(1800),
        RedirectUri = Url.Content("~/")
      };

      return Challenge(authProps, Constant.OIDC);
    }

    public async Task OnGetLogout()
    {
      System.Diagnostics.Debug.WriteLine("\n_Host OnGetLogout");
      var authProps = new AuthenticationProperties
      {
        RedirectUri = Url.Content("~/")
      };
      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
      await HttpContext.SignOutAsync(Constant.OIDC, authProps);
    }
  }
}