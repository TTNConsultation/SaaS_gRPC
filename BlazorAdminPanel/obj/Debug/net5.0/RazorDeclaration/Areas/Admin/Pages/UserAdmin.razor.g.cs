// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace BlazorAdminPanel.Admin
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\_Imports.razor"
using BlazorAdminPanel;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\_Imports.razor"
using BlazorAdminPanel.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\_Imports.razor"
using Constant;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\_Imports.razor"
using DbContext.Interfaces;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\UserAdmin.razor"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/Admin/UserAdmin")]
    public partial class UserAdmin : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 34 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\UserAdmin.razor"
       
  [Inject] AuthenticationStateProvider authenticationStateProvider { get; set; }
  public static AuthenticationState AuState = null;

  [Inject] UserManager<IdentityUser> userManager { get; set; }
  [Inject] IJSRuntime JS { get; set; }

  IList<IdentityUser> users;
  Dictionary<string, Boolean> OpenUsers;

  protected override void OnInitialized()
  {
    AuState = authenticationStateProvider.GetAuthenticationStateAsync().Result;
    if (!AuState.User.IsInRole("Admin"))
    {
      throw new Exception("You have no right to access this page");
    }

    users = userManager.Users.ToList();
    OpenUsers = new Dictionary<string, Boolean>();
    foreach (IdentityUser OneUser in users)
    {
      OpenUsers.Add(OneUser.Id, false);
    }
    base.OnInitialized();
  }

  protected string display(string UserId)
  {
    return OpenUsers[UserId] ? "block" : "none";
  }

  protected string visibility(string UserId)
  {
    return OpenUsers[UserId] ? "visible" : "hidden";
  }

  private void Toggle(string UserId)
  {
    OpenUsers[UserId] = !OpenUsers[UserId];
    StateHasChanged();
  }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
