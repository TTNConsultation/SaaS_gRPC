#pragma checksum "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Shared\MainLayout.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e3777aa690866c54b96a4bd9910a8a2a1fa6c933"
// <auto-generated/>
#pragma warning disable 1591
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
    public partial class MainLayout : LayoutComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "sidebar");
            __builder.OpenComponent<BlazorAdminPanel.Admin.NavMenu>(2);
            __builder.CloseComponent();
            __builder.CloseElement();
            __builder.AddMarkupContent(3, "\r\n\r\n");
            __builder.OpenElement(4, "div");
            __builder.AddAttribute(5, "class", "main");
            __builder.OpenElement(6, "div");
            __builder.AddAttribute(7, "class", "top-row px-4");
            __builder.AddAttribute(8, "style", "cursor:pointer");
            __builder.OpenElement(9, "a");
            __builder.AddAttribute(10, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 10 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Shared\MainLayout.razor"
                  ReturnFromAdminPanel

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(11, "target", "_blank");
            __builder.AddContent(12, "Return to site");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(13, "\r\n\r\n  ");
            __builder.OpenElement(14, "div");
            __builder.AddAttribute(15, "class", "content px-4");
            __builder.AddContent(16, 
#nullable restore
#line 14 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Shared\MainLayout.razor"
     Body

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 18 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Shared\MainLayout.razor"
       
  [Inject] NavigationManager navigationManager { get; set; }

  protected void ReturnFromAdminPanel()
  {
    navigationManager.NavigateTo("/", forceLoad: true);
  }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
