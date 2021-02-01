// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace BlazorAdminPanel.Shared
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
using Protos.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\_Imports.razor"
using Protos.Shared.Interfaces;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Shared\NavMenu.razor"
using Microsoft.Extensions.Logging;

#line default
#line hidden
#nullable disable
    public partial class NavMenu : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 39 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Shared\NavMenu.razor"
       

  [Inject] AuthenticationStateProvider authenticationStateProvider { get; set; }
  [Inject] ILogger<NavMenu> Log { get; set; }
  [Inject] NavigationManager navigationManager { get; set; }

  public static AuthenticationState AuState = null;

  protected override async Task OnInitializedAsync()
  {
    AuState = await authenticationStateProvider.GetAuthenticationStateAsync();
    Log.LogInformation($"MainNav.AuthenticationStateProvider.User (OnInitializedAsync) ={AuState.User.Identity.Name}");
  }

  protected void AdminRedirect()
  {
    navigationManager.NavigateTo("/Admin/", forceLoad: true);
  }

  protected void RestaurantRedirect()
  {
    navigationManager.NavigateTo("/Restaurant", forceLoad: true);
  }

  private bool collapseNavMenu = true;

  private string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

  private void ToggleNavMenu()
  {
    collapseNavMenu = !collapseNavMenu;
  }


#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
