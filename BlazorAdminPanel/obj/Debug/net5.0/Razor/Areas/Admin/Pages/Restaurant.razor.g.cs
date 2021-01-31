#pragma checksum "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\Restaurant.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4dbd2c083bb2a7932f30725413f04eba2162ea9e"
// <auto-generated/>
#pragma warning disable 1591
namespace BlazorAdminPanel.Areas.Admin.Pages
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
using DbContext.Interface;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/Admin/Restaurant")]
    public partial class Restaurant : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h1>Restaurants</h1>\r\n\r\n");
            __builder.AddMarkupContent(1, "<p>This component demonstrates fetching data from a service.</p>");
#nullable restore
#line 7 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\Restaurant.razor"
 if (_rests == null)
{

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(2, "<p><em>Loading...</em></p>");
#nullable restore
#line 10 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\Restaurant.razor"
}
else
{

#line default
#line hidden
#nullable disable
            __builder.OpenElement(3, "table");
            __builder.AddAttribute(4, "class", "table");
            __builder.AddMarkupContent(5, "<thead><tr><th>Restaurant</th>\r\n        <th>Location</th>\r\n        <th>Unit</th>\r\n        <th>Street Name</th>\r\n        <th>Phone</th>\r\n        <th>Link</th></tr></thead>\r\n    ");
            __builder.OpenElement(6, "tbody");
#nullable restore
#line 25 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\Restaurant.razor"
       foreach (var _rest in _rests.Values)
      {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(7, "tr");
            __builder.OpenElement(8, "td");
            __builder.AddContent(9, 
#nullable restore
#line 28 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\Restaurant.razor"
               _rest.Name

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(10, "\r\n          ");
            __builder.OpenElement(11, "td");
            __builder.AddContent(12, 
#nullable restore
#line 29 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\Restaurant.razor"
               _rest.RestaurantLocationName

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(13, "\r\n          ");
            __builder.OpenElement(14, "td");
            __builder.AddContent(15, 
#nullable restore
#line 30 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\Restaurant.razor"
               _rest.LocationUnit

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(16, "\r\n          ");
            __builder.OpenElement(17, "td");
            __builder.AddContent(18, 
#nullable restore
#line 31 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\Restaurant.razor"
               _rest.LocationStreetName

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(19, "\r\n          ");
            __builder.OpenElement(20, "td");
            __builder.AddContent(21, 
#nullable restore
#line 32 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\Restaurant.razor"
               _rest.RestaurantLocationPhoneNumber

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(22, "\r\n          ");
            __builder.OpenElement(23, "td");
            __builder.OpenElement(24, "a");
            __builder.AddAttribute(25, "href", 
#nullable restore
#line 33 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\Restaurant.razor"
                        _rest.LocationLink

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(26, "class", "material-icons");
            __builder.AddAttribute(27, "style", "font-size:24px;color:blue");
            __builder.AddContent(28, "add_location");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 35 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\Restaurant.razor"
      }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 38 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\Restaurant.razor"
}

#line default
#line hidden
#nullable disable
            __builder.AddContent(29, ";");
        }
        #pragma warning restore 1998
#nullable restore
#line 40 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\Restaurant.razor"
       
  [Inject] IDbContext dbContext { get; set; }
  [Inject] Saas.App app { get; set; }
  [Inject] AuthenticationStateProvider authenticationStateProvider { get; set; }

  public static AuthenticationState AuState = null;

  private Saas.Message.Administrator.Restaurants _rests;

  protected override void OnInitialized()
  {
    AuState = authenticationStateProvider.GetAuthenticationStateAsync().Result;
    if (!AuState.User.IsInRole("Admin"))
    {
      throw new Exception("You have no right to access this page");
    }

    using var sp = dbContext.Read<Saas.Message.Administrator.Restaurant>(app.RefDatas.AppSetting.Id, AuState.User, OperationType.R);
    _rests = (sp.IsReady) ? new Saas.Message.Administrator.Restaurants(sp.Read("pho")) : null;
  }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591