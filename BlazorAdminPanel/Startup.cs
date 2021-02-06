using BlazorAdminPanel.Areas.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Constant;
using DbContext.MsSql;
using DbContext.Interfaces;

namespace BlazorAdminPanel
{
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
      services.AddDbContext<IdentityDbContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString(StrVal.LOGIN)));

      services.AddDefaultIdentity<IdentityUser>(options =>
       {
         options.Password.RequiredLength = 4;
         options.Password.RequireUppercase = false;
         options.Password.RequireLowercase = false;
         options.Password.RequireUppercase = false;
         options.Password.RequireNonAlphanumeric = false;
         options.SignIn.RequireConfirmedEmail = true;
       })
       .AddRoles<IdentityRole>()
       .AddEntityFrameworkStores<IdentityDbContext>()
       .AddDefaultTokenProviders();

      services.AddDatabaseDeveloperPageExceptionFilter();

      services.AddRazorPages();
      services.AddServerSideBlazor();
      services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

      services.AddSingleton<IDbContext, StoreProcedure>();
      services.AddSingleton<AppData>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      //Seed.AddTestUsers(identityDbContextOptions, userManager, roleManager);

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
        endpoints.MapControllers();
        endpoints.MapBlazorHub();
        endpoints.MapFallbackToPage("/_Host");
      });
    }
  }
}