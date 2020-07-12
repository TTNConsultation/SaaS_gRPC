using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Saas.Services;
using Saas.Entity.App;
using Microsoft.AspNetCore.Http;
using IdentityServer4.AccessTokenValidation;

using Dal;
using Dal.Sp;

namespace Saas
{
  public class Startup
  {
    private readonly bool isDevelopment;
    private readonly IConfiguration config;

    public Startup(IWebHostEnvironment env, IConfiguration cfg)
    {
      isDevelopment = env.IsDevelopment();
      config = cfg;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddGrpc(options => options.EnableDetailedErrors = true);

      var authSrv = config.GetSection("IdentityServer4");
      services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
              .AddIdentityServerAuthentication(options =>
        {
          options.Authority = authSrv["Url"];
          options.ApiName = authSrv["ApiName"];
          options.ApiSecret = authSrv["ApiSecret"];
        });

      services.AddAuthorization(options =>
      {
        options.AddPolicy("admin", policy =>
        {
          policy.RequireAuthenticatedUser();
          policy.RequireClaim("scope", "aaf.admin");
          policy.RequireClaim("role", "administrator");
        });
      });

      services.AddCors(o => o.AddPolicy(Constant.CorsAllowedPolicy, builder =>
      {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .WithExposedHeaders("Grpc-Status", "Grpc-Message");
      }));

      services.AddSingleton<IConnectionManager, ConnectionManager>();
      services.AddSingleton<ICollectionMapper, CollectionMapper>();
      services.AddSingleton<ICollectionSpInfo, CollectionSpInfo>();
      services.AddSingleton<IContext, Context>();
      services.AddSingleton<ReferenceData>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app)
    {
      if (isDevelopment)
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseGrpcWeb();
      app.UseCors();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapGrpcService<AppService>().RequireCors(Constant.CorsAllowedPolicy).EnableGrpcWeb();
        endpoints.MapGrpcService<RestaurantService>().RequireCors(Constant.CorsAllowedPolicy).EnableGrpcWeb();
        endpoints.MapGrpcService<TableService>().RequireCors(Constant.CorsAllowedPolicy).EnableGrpcWeb();
        endpoints.MapGrpcService<ItemService>().RequireCors(Constant.CorsAllowedPolicy).EnableGrpcWeb();
        endpoints.MapGrpcService<RestaurantMenuService>().RequireCors(Constant.CorsAllowedPolicy).EnableGrpcWeb();
        endpoints.MapGrpcService<MenuItemService>().RequireCors(Constant.CorsAllowedPolicy).EnableGrpcWeb();
        endpoints.MapGrpcService<TestService>().RequireCors(Constant.CorsAllowedPolicy).EnableGrpcWeb();

        //endpoints.MapControllers();

        endpoints.MapGet("/", async context => await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909").ConfigureAwait(false));
      });
    }
  }
}