#pragma checksum "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Pages\Items.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f17a06d728f99df9b4e68cf84bd68fcd6eb8be97"
// <auto-generated/>
#pragma warning disable 1591
namespace BlazorAdminPanel.Pages
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
#line 2 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Pages\Items.razor"
using Protos = Protos.Shared.Message.Administrator;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/Restaurant/{RestaurantId:int}/Items")]
    public partial class Items : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h3>Item</h3>\r\n\r\n;");
        }
        #pragma warning restore 1998
#nullable restore
#line 6 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Pages\Items.razor"
       
  [Inject] IDbContext dbContext { get; set; }
  [Inject] AppData app { get; set; }
  [Inject] AuthenticationStateProvider authenticationStateProvider { get; set; }

  [Parameter]
  public int RestaurantId { get; set; }

  public static AuthenticationState AuState = null;

  private Protos.Items _items;

  protected override void OnInitialized()
  {
    AuState = authenticationStateProvider.GetAuthenticationStateAsync().Result;
    if (!AuState.User.IsInRole(Constant.ADMIN))
    {
      throw new Exception("You have no right to access this page");
    }

    using var sp = dbContext.Read<Protos.Item>(app.RefDatas.AppSetting.Id, AuState.User, OperationType.R);
    _items = (sp.IsReady) ? new Protos.Items(sp.Read<Protos.Restaurant>(RestaurantId)) : null;
  }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
