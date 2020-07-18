// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IdentityServer4;
using IdentityServer4.Services;

namespace IdentityServer
{
  public class Startup
  {
    public IWebHostEnvironment Environment { get; }

    public Startup(IWebHostEnvironment environment)
    {
      Environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      // uncomment, if you want to add an MVC-based UI
      services.AddControllersWithViews();

      var builder = services.AddIdentityServer(options =>
      {
        // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
        options.EmitStaticAudienceClaim = true;
      })
          .AddInMemoryIdentityResources(Config.IdentityResources)
          .AddInMemoryApiResources(Config.ApiResources)
          .AddInMemoryApiScopes(Config.ApiScopes)
          .AddInMemoryClients(Config.Clients)
          .AddTestUsers(TestUsers.Users);

      //services.AddAuthentication()
      //    .AddGoogle("Google", options =>
      //    {
      //      options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

      //      options.ClientId = "964468178648-02b6d5okdoo055aeq58np4jbq3rnoo96.apps.googleusercontent.com";
      //      options.ClientSecret = "bUuNq643Jb0qGn2Bdv3kDNYx";
      //    });

      //services.AddSingleton<ICorsPolicyService, CorsPolicyService>();

      // not recommended for production - you need to store your key material somewhere secure
      builder.AddDeveloperSigningCredential();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
      if (Environment.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      // uncomment if you want to add MVC
      app.UseStaticFiles();
      app.UseRouting();

      app.UseIdentityServer();
      // uncomment, if you want to add MVC
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapDefaultControllerRoute();
      });
    }
  }
}