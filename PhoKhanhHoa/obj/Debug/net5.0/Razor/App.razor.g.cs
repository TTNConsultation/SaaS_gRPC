#pragma checksum "D:\Dev\SaaS_AllAboutFood\PhoKhanhHoa\App.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2abe7591237154d75c129d048a59a47b15c05a55"
// <auto-generated/>
#pragma warning disable 1591
namespace PhoKhanhHoa
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\Dev\SaaS_AllAboutFood\PhoKhanhHoa\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Dev\SaaS_AllAboutFood\PhoKhanhHoa\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Dev\SaaS_AllAboutFood\PhoKhanhHoa\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Dev\SaaS_AllAboutFood\PhoKhanhHoa\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Dev\SaaS_AllAboutFood\PhoKhanhHoa\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Dev\SaaS_AllAboutFood\PhoKhanhHoa\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Dev\SaaS_AllAboutFood\PhoKhanhHoa\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Dev\SaaS_AllAboutFood\PhoKhanhHoa\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Dev\SaaS_AllAboutFood\PhoKhanhHoa\_Imports.razor"
using PhoKhanhHoa;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\Dev\SaaS_AllAboutFood\PhoKhanhHoa\_Imports.razor"
using PhoKhanhHoa.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\Dev\SaaS_AllAboutFood\PhoKhanhHoa\_Imports.razor"
using Saas.gRPC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\Dev\SaaS_AllAboutFood\PhoKhanhHoa\_Imports.razor"
using Google.Protobuf.WellKnownTypes;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "D:\Dev\SaaS_AllAboutFood\PhoKhanhHoa\_Imports.razor"
using DbContext.Interface;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "D:\Dev\SaaS_AllAboutFood\PhoKhanhHoa\_Imports.razor"
[Authorize]

#line default
#line hidden
#nullable disable
    public partial class App : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<Microsoft.AspNetCore.Components.Routing.Router>(0);
            __builder.AddAttribute(1, "AppAssembly", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Reflection.Assembly>(
#nullable restore
#line 3 "D:\Dev\SaaS_AllAboutFood\PhoKhanhHoa\App.razor"
                      typeof(Program).Assembly

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(2, "PreferExactMatches", 
#nullable restore
#line 3 "D:\Dev\SaaS_AllAboutFood\PhoKhanhHoa\App.razor"
                                                                     true

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(3, "Found", (Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.RouteData>)((routeData) => (__builder2) => {
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Authorization.AuthorizeRouteView>(4);
                __builder2.AddAttribute(5, "RouteData", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.RouteData>(
#nullable restore
#line 5 "D:\Dev\SaaS_AllAboutFood\PhoKhanhHoa\App.razor"
                                    routeData

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(6, "DefaultLayout", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Type>(
#nullable restore
#line 5 "D:\Dev\SaaS_AllAboutFood\PhoKhanhHoa\App.razor"
                                                               typeof(MainLayout)

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(7, "NotAuthorized", (Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Authorization.AuthenticationState>)((context) => (__builder3) => {
#nullable restore
#line 7 "D:\Dev\SaaS_AllAboutFood\PhoKhanhHoa\App.razor"
          
          // part 2: add this
          NavManager.NavigateTo("/Logout", true);
        

#line default
#line hidden
#nullable disable
                }
                ));
                __builder2.AddAttribute(8, "Authorizing", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(9, "<h1>Authentication in progress</h1>\r\n        ");
                    __builder3.AddMarkupContent(10, "<p>Only visible while authentication is in progress.</p>");
                }
                ));
                __builder2.CloseComponent();
            }
            ));
            __builder.AddAttribute(11, "NotFound", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Authorization.CascadingAuthenticationState>(12);
                __builder2.AddAttribute(13, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.OpenComponent<Microsoft.AspNetCore.Components.LayoutView>(14);
                    __builder3.AddAttribute(15, "Layout", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Type>(
#nullable restore
#line 20 "D:\Dev\SaaS_AllAboutFood\PhoKhanhHoa\App.razor"
                           typeof(MainLayout)

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(16, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.AddMarkupContent(17, "<h1>Sorry</h1>\r\n        ");
                        __builder4.AddMarkupContent(18, "<p>Sorry, there\'s nothing at this address.</p>");
                    }
                    ));
                    __builder3.CloseComponent();
                }
                ));
                __builder2.CloseComponent();
            }
            ));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavManager { get; set; }
    }
}
#pragma warning restore 1591
