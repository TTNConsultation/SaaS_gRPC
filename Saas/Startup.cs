using System.Threading.Tasks;
using System.Security.Claims;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Certificate;

using Microsoft.AspNetCore.Http;

using Saas.Services;
using Saas.Dal;

using DbContext;
using DbContext.Interface;

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

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddGrpc(options => options.EnableDetailedErrors = true);

      //services.AddAuthorization(options =>
      //{
      //  options.AddPolicy("admin", policy =>
      //  {
      //    policy.RequireAuthenticatedUser();
      //    policy.RequireClaim("scope", "aaf.admin");
      //    policy.RequireClaim("role", "administrator");
      //  });
      //});

      //var auth = config.GetSection("IdentityServer");
      //services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
      //        .AddIdentityServerAuthentication(options =>
      //        {
      //          options.Authority = auth["Url"];
      //          options.ApiName = auth["ApiName"];
      //          options.ApiSecret = auth["ApiSecret"];
      //        });

      services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
              .AddCertificate(options =>
              {
                options.Events = new CertificateAuthenticationEvents
                {
                  OnCertificateValidated = context =>
                  {
                    var claims = new[]
                    {
                                new Claim(
                                    ClaimTypes.NameIdentifier,
                                    context.ClientCertificate.Subject,
                                    ClaimValueTypes.String,
                                    context.Options.ClaimsIssuer),
                                new Claim(ClaimTypes.Thumbprint,
                                    context.ClientCertificate.Thumbprint,
                                    ClaimValueTypes.String,
                                    context.Options.ClaimsIssuer)
                      };

                    context.Principal.AddIdentity(new ClaimsIdentity(claims, context.Scheme.Name));
                    context.Success();

                    return Task.CompletedTask;
                  }
                };
              });

      services.AddCors(o => o.AddPolicy(Constant.CORSPOLICY, builder =>
      {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
      }));

      services.AddSingleton<IConnectionManager, ConnectionManager>();
      services.AddSingleton<ICollectionMapper, CollectionMapper>();
      services.AddSingleton<ICollectionProcedure, CollectionProcedure>();
      services.AddSingleton<IDbContext, StoreProcedure>();
      services.AddSingleton<App>();
    }

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

      app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
      app.UseCors();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapGrpcService<AppData>().RequireCors(Constant.CORSPOLICY);
        endpoints.MapGrpcService<RestaurantService>().RequireCors(Constant.CORSPOLICY);
        endpoints.MapGrpcService<TableService>().RequireCors(Constant.CORSPOLICY);
        endpoints.MapGrpcService<ItemService>().RequireCors(Constant.CORSPOLICY);
        endpoints.MapGrpcService<RestaurantMenuService>().RequireCors(Constant.CORSPOLICY);
        endpoints.MapGrpcService<MenuItemService>().RequireCors(Constant.CORSPOLICY);

        endpoints.MapGet("/", async context => await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909").ConfigureAwait(false));
      });
    }
  }
}