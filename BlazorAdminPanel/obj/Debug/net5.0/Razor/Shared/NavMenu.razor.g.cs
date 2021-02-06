#pragma checksum "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Shared\NavMenu.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "622d9c6cdb1fdd85304d2d9aa821414df108a532"
// <auto-generated/>
#pragma warning disable 1591
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
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "top-row pl-4 navbar navbar-dark");
            __builder.AddMarkupContent(2, "<a class=\"navbar-brand\" href>BlazorAdminPanel</a>\r\n  ");
            __builder.OpenElement(3, "button");
            __builder.AddAttribute(4, "class", "navbar-toggler");
            __builder.AddAttribute(5, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 5 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Shared\NavMenu.razor"
                                           ToggleNavMenu

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(6, "<span class=\"navbar-toggler-icon\"></span>");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(7, "\r\n\r\n");
            __builder.OpenElement(8, "div");
            __builder.AddAttribute(9, "class", 
#nullable restore
#line 10 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Shared\NavMenu.razor"
             NavMenuCssClass

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(10, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 10 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Shared\NavMenu.razor"
                                        ToggleNavMenu

#line default
#line hidden
#nullable disable
            ));
            __builder.OpenElement(11, "ul");
            __builder.AddAttribute(12, "class", "nav flex-column");
            __builder.OpenElement(13, "li");
            __builder.AddAttribute(14, "class", "nav-item px-3");
            __builder.OpenComponent<Microsoft.AspNetCore.Components.Routing.NavLink>(15);
            __builder.AddAttribute(16, "class", "nav-link");
            __builder.AddAttribute(17, "href", "");
            __builder.AddAttribute(18, "Match", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.Routing.NavLinkMatch>(
#nullable restore
#line 13 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Shared\NavMenu.razor"
                                               NavLinkMatch.All

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(19, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.AddMarkupContent(20, "<span class=\"oi oi-home\" aria-hidden=\"true\"></span> Home\r\n      ");
            }
            ));
            __builder.CloseComponent();
            __builder.CloseElement();
#nullable restore
#line 22 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Shared\NavMenu.razor"
     if (AuState.User.IsInRole(Constant.ADMIN))
    {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(21, "li");
            __builder.AddAttribute(22, "class", "nav-item px-3");
            __builder.AddAttribute(23, "style", "cursor:pointer");
            __builder.OpenComponent<Microsoft.AspNetCore.Components.Routing.NavLink>(24);
            __builder.AddAttribute(25, "class", "nav-link");
            __builder.AddAttribute(26, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 25 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Shared\NavMenu.razor"
                                             AdminRedirect

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(27, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.AddMarkupContent(28, "<img src=\"Images/keys.png\" style=\"width:20px;height:20px;margin:3px;\">\r\n          Admin panel\r\n        ");
            }
            ));
            __builder.CloseComponent();
            __builder.CloseElement();
            __builder.AddMarkupContent(29, "\r\n      ");
            __builder.OpenElement(30, "li");
            __builder.AddAttribute(31, "class", "nav-item px-3");
            __builder.AddAttribute(32, "style", "cursor:pointer");
            __builder.OpenComponent<Microsoft.AspNetCore.Components.Routing.NavLink>(33);
            __builder.AddAttribute(34, "class", "nav-link");
            __builder.AddAttribute(35, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 31 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Shared\NavMenu.razor"
                                             RestaurantRedirect

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(36, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.AddMarkupContent(37, "\r\n          Restaurant\r\n        ");
            }
            ));
            __builder.CloseComponent();
            __builder.CloseElement();
#nullable restore
#line 35 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Shared\NavMenu.razor"
    }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.CloseElement();
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
