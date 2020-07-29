using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IdentityServer4;
using IdentityServer.Entities;

using Dal.Sp;
using Microsoft.AspNetCore.Authentication.Certificate;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace IdentityServer
{
  public class Startup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      // uncomment, if you want to add an MVC-based UI
      services.AddControllersWithViews();

      services.AddGrpc();

      var builder = services.AddIdentityServer(options =>
      {
        // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
        options.EmitStaticAudienceClaim = true;
      })
                    .AddInMemoryIdentityResources(Config.IdentityResources)
                    .AddInMemoryApiResources(Config.ApiResources)
                    .AddInMemoryApiScopes(Config.ApiScopes)
                    .AddInMemoryClients(Config.Clients)
                    .AddTestUsers(TestUsers.Users)
                    .AddDeveloperSigningCredential();

      services.AddAuthentication().AddGoogle("Google", options =>
          {
            options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

            options.ClientId = "964468178648-02b6d5okdoo055aeq58np4jbq3rnoo96.apps.googleusercontent.com";
            options.ClientSecret = "bUuNq643Jb0qGn2Bdv3kDNYx";
          });

      services.AddSingleton<IConnectionManager, ConnectionManager>()
              .AddSingleton<ICollectionMapper, CollectionMapper>()
              .AddSingleton<ICollectionSpProperty, CollectionSpProperty>()
              .AddSingleton<IDbContext, DbContext>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseStaticFiles();
      app.UseRouting();

      app.UseIdentityServer();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapGet("/", async context =>
        {
          await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
        });

        endpoints.MapDefaultControllerRoute();
      });
    }
  }
}