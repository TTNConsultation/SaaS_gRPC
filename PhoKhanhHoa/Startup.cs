using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Saas.Dal;
using DbContext;
using DbContext.Interface;

namespace PhoKhanhHoa
{
  public static class Constant
  {
    public const string OIDC = "oidc";
    public const string API_NAME = "ApiName";
    public const string API_SECRET = "ApiSecret";
    public const string IDENTITY_SERVER = "IdentityServer";
    public const string URI = "Uri";
    public const string CLIENT_ID = "ClientId";
    public const string CLIENT_SECRET = "ClientSecret";
    public const string SCOPE = "Scope";
    public const string CODE = "code";
  }

  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddRazorPages();
      services.AddServerSideBlazor();

      // add this
      var isrvCfg = Configuration.GetSection(Constant.IDENTITY_SERVER);
      services.AddAuthentication(options =>
      {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = Constant.OIDC;
      })
      .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
      .AddOpenIdConnect(Constant.OIDC, options =>
      {
        options.Authority = isrvCfg[Constant.URI];
        options.ClientId = isrvCfg[Constant.CLIENT_ID];
        options.ClientSecret = isrvCfg[Constant.CLIENT_SECRET];
        options.ResponseType = Constant.CODE;

        foreach (var s in isrvCfg[Constant.SCOPE].Split(";"))
        {
          options.Scope.Add(s);
        }

        options.SaveTokens = true;

        // for API add offline_access scope to get refresh_token
        options.GetClaimsFromUserInfoEndpoint = true;

        options.Events = new OpenIdConnectEvents
        {
          // called if user clicks Cancel during login
          OnAccessDenied = context =>
          {
            context.HandleResponse();
            context.Response.Redirect("/");
            return Task.CompletedTask;
          }
        };
      });

      services.AddSingleton<IConnectionManager, ConnectionManager>();
      services.AddSingleton<ICollectionMapper, CollectionMapper>();
      services.AddSingleton<ICollectionProcedure, CollectionProcedure>();
      services.AddSingleton<IDbContext, StoreProcedure>();

      services.AddSingleton<Saas.App>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapBlazorHub();
        endpoints.MapFallbackToPage("/_Host");
      });
    }
  }
}