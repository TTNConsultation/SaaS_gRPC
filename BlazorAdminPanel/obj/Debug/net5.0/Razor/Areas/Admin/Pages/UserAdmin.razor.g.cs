#pragma checksum "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\UserAdmin.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a7b144a6ac03b8d1646b7f2501e557f9bb20a7bc"
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
using DbContext.Interface;

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
            __builder.AddMarkupContent(0, "<h3>Users</h3>\r\n\r\n");
            __builder.OpenElement(1, "ul");
#nullable restore
#line 8 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\UserAdmin.razor"
   foreach (IdentityUser OneUser in users)
  {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(2, "li");
            __builder.AddMarkupContent(3, "<img src=\"images/remove.gif\">\r\n      ");
            __builder.OpenElement(4, "a");
            __builder.AddAttribute(5, "style", "cursor:pointer");
            __builder.AddAttribute(6, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 12 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\UserAdmin.razor"
                                            ()=>@Toggle(OneUser.Id)

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(7, 
#nullable restore
#line 12 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\UserAdmin.razor"
                                                                       OneUser.Email

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(8, "\r\n      ");
            __builder.OpenElement(9, "table");
            __builder.AddAttribute(10, "style", "display:" + (
#nullable restore
#line 13 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\UserAdmin.razor"
                             display(OneUser.Id)

#line default
#line hidden
#nullable disable
            ) + ";visibility:" + (
#nullable restore
#line 13 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\UserAdmin.razor"
                                                             visibility(OneUser.Id)

#line default
#line hidden
#nullable disable
            ));
            __builder.OpenElement(11, "tr");
            __builder.AddMarkupContent(12, "<td>AccessFailedCount</td>");
            __builder.OpenElement(13, "td");
            __builder.AddContent(14, 
#nullable restore
#line 14 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\UserAdmin.razor"
                                           OneUser.AccessFailedCount

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(15, "\r\n        ");
            __builder.OpenElement(16, "tr");
            __builder.AddMarkupContent(17, "<td>ConcurrencyStamp</td>");
            __builder.OpenElement(18, "td");
            __builder.AddContent(19, 
#nullable restore
#line 15 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\UserAdmin.razor"
                                          OneUser.ConcurrencyStamp

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(20, "\r\n        ");
            __builder.OpenElement(21, "tr");
            __builder.AddMarkupContent(22, "<td>Email</td>");
            __builder.OpenElement(23, "td");
            __builder.AddContent(24, 
#nullable restore
#line 16 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\UserAdmin.razor"
                               OneUser.Email

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(25, "\r\n        ");
            __builder.OpenElement(26, "tr");
            __builder.AddMarkupContent(27, "<td>EmailConfirmed</td>");
            __builder.OpenElement(28, "td");
            __builder.AddContent(29, 
#nullable restore
#line 17 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\UserAdmin.razor"
                                        OneUser.EmailConfirmed

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(30, "\r\n        ");
            __builder.OpenElement(31, "tr");
            __builder.AddMarkupContent(32, "<td>Id</td>");
            __builder.OpenElement(33, "td");
            __builder.AddContent(34, 
#nullable restore
#line 18 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\UserAdmin.razor"
                            OneUser.Id

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(35, "\r\n        ");
            __builder.OpenElement(36, "tr");
            __builder.AddMarkupContent(37, "<td>LockoutEnabled</td>");
            __builder.OpenElement(38, "td");
            __builder.AddContent(39, 
#nullable restore
#line 19 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\UserAdmin.razor"
                                        OneUser.LockoutEnabled

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(40, "\r\n        ");
            __builder.OpenElement(41, "tr");
            __builder.AddMarkupContent(42, "<td>LockoutEnd</td>");
            __builder.OpenElement(43, "td");
            __builder.AddContent(44, 
#nullable restore
#line 20 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\UserAdmin.razor"
                                    OneUser.LockoutEnd

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(45, "\r\n        ");
            __builder.OpenElement(46, "tr");
            __builder.AddMarkupContent(47, "<td>NormalizedEmail</td>");
            __builder.OpenElement(48, "td");
            __builder.AddContent(49, 
#nullable restore
#line 21 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\UserAdmin.razor"
                                         OneUser.NormalizedEmail

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(50, "\r\n        ");
            __builder.OpenElement(51, "tr");
            __builder.AddMarkupContent(52, "<td>NormalizedUserName</td>");
            __builder.OpenElement(53, "td");
            __builder.AddContent(54, 
#nullable restore
#line 22 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\UserAdmin.razor"
                                            OneUser.NormalizedUserName

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(55, "\r\n        ");
            __builder.OpenElement(56, "tr");
            __builder.AddMarkupContent(57, "<td>PasswordHash</td>");
            __builder.OpenElement(58, "td");
            __builder.AddContent(59, 
#nullable restore
#line 23 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\UserAdmin.razor"
                                      OneUser.PasswordHash

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(60, "\r\n        ");
            __builder.OpenElement(61, "tr");
            __builder.AddMarkupContent(62, "<td>PhoneNumber</td>");
            __builder.OpenElement(63, "td");
            __builder.AddContent(64, 
#nullable restore
#line 24 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\UserAdmin.razor"
                                     OneUser.PhoneNumber

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(65, "\r\n        ");
            __builder.OpenElement(66, "tr");
            __builder.AddMarkupContent(67, "<td>PhoneNumberConfirmed</td>");
            __builder.OpenElement(68, "td");
            __builder.AddContent(69, 
#nullable restore
#line 25 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\UserAdmin.razor"
                                              OneUser.PhoneNumberConfirmed

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(70, "\r\n        ");
            __builder.OpenElement(71, "tr");
            __builder.AddMarkupContent(72, "<td>SecurityStamp</td>");
            __builder.OpenElement(73, "td");
            __builder.AddContent(74, 
#nullable restore
#line 26 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\UserAdmin.razor"
                                       OneUser.SecurityStamp

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(75, "\r\n        ");
            __builder.OpenElement(76, "tr");
            __builder.AddMarkupContent(77, "<td>TwoFactorEnabled</td>");
            __builder.OpenElement(78, "td");
            __builder.AddContent(79, 
#nullable restore
#line 27 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\UserAdmin.razor"
                                          OneUser.TwoFactorEnabled

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(80, "\r\n        ");
            __builder.OpenElement(81, "tr");
            __builder.AddMarkupContent(82, "<td>UserName</td>");
            __builder.OpenElement(83, "td");
            __builder.AddContent(84, 
#nullable restore
#line 28 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\UserAdmin.razor"
                                  OneUser.UserName

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 31 "D:\Dev\SaaS_AllAboutFood\BlazorAdminPanel\Areas\Admin\Pages\UserAdmin.razor"
  }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
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
