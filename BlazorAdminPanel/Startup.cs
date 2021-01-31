using BlazorAdminPanel.Areas.Identity;
using BlazorAdminPanel.Data;
using DbContext;
using DbContext.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Saas.Dal;

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
      services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(
              Configuration.GetConnectionString(Constant.LOGIN)));

      services.AddDefaultIdentity<IdentityUser>(options =>
       {
         options.Password.RequiredLength = 4;
         options.Password.RequireUppercase = false;
         options.Password.RequireLowercase = false;
         options.Password.RequireUppercase = false;
         options.Password.RequireNonAlphanumeric = false;
         options.SignIn.RequireConfirmedEmail = true;
       }).AddRoles<IdentityRole>()
       .AddEntityFrameworkStores<ApplicationDbContext>()
       .AddDefaultTokenProviders();

      services.AddRazorPages();
      services.AddServerSideBlazor();
      services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

      services.AddSingleton<IConnectionManager, ConnectionManager>();
      services.AddSingleton<ICollectionMapper, CollectionMapper>();
      services.AddSingleton<ICollectionProcedure, CollectionProcedure>();
      services.AddSingleton<IDbContext, StoreProcedure>();

      services.AddSingleton<Saas.App>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      //Seed.AddTestUsers(identityDbContextOptions, userManager, roleManager);

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
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