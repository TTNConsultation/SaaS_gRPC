﻿using DbContext;
using DbContext.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Protos.Shared.Dal;
using Saas.Services;

using Protos.Shared;

namespace Shared
{
  public class Startup
  {
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
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

      //services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
      //        .AddCertificate(options =>
      //        {
      //          options.Events = new CertificateAuthenticationEvents
      //          {
      //            OnCertificateValidated = context =>
      //            {
      //              var claims = new[]
      //              {
      //                          new Claim(
      //                              ClaimTypes.NameIdentifier,
      //                              context.ClientCertificate.Subject,
      //                              ClaimValueTypes.String,
      //                              context.Options.ClaimsIssuer),
      //                          new Claim(ClaimTypes.Thumbprint,
      //                              context.ClientCertificate.Thumbprint,
      //                              ClaimValueTypes.String,
      //                              context.Options.ClaimsIssuer)
      //                };

      //              context.Principal.AddIdentity(new ClaimsIdentity(claims, context.Scheme.Name));
      //              context.Success();

      //              return Task.CompletedTask;
      //            }
      //          };
      //        });

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
      services.AddSingleton<Protos.Shared.AppData>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      //app.UseAuthentication();
      //app.UseAuthorization();

      app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
      app.UseCors();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapGrpcService<Saas.Services.AppData>().RequireCors(Constant.CORSPOLICY);
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