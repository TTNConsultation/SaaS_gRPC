﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

using IdentityServer4.AccessTokenValidation;

using Saas.Services;
using Saas.Entity;
using Dal.Sp;
using Microsoft.AspNetCore.Authentication.Certificate;
using System.Security.Cryptography.X509Certificates;
using System.Security.Claims;
using System.Threading.Tasks;

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

      services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
              .AddCertificate(options =>
              {
                // Not recommended in production environments. The example is using a self-signed test certificate.
                options.RevocationMode = X509RevocationMode.NoCheck;
                options.AllowedCertificateTypes = CertificateTypes.All;

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
                        new Claim(
                            ClaimTypes.Name,
                            context.ClientCertificate.Subject,
                            ClaimValueTypes.String,
                            context.Options.ClaimsIssuer)
                    };

                    context.Principal = new ClaimsPrincipal(new ClaimsIdentity(claims, context.Scheme.Name));
                    context.Success();

                    return Task.CompletedTask;
                  }
                };
              });

      var auth = config.GetSection("IdentityServer");
      services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
              .AddIdentityServerAuthentication(options =>
              {
                options.Authority = auth["Url"];
                options.ApiName = auth["ApiName"];
                options.ApiSecret = auth["ApiSecret"];
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
      services.AddSingleton<IDbContext, DbContext>();

      services.AddSingleton<App>();
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

        //endpoints.MapControllers();

        endpoints.MapGet("/", async context => await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909").ConfigureAwait(false));
      });
    }
  }
}