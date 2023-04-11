using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildingMaintenance.CommonCode;
using BuildingMaintenance.CommonCode.Helpers;
using BuildingMaintenance.Models.Entities;
using BuildingMaintenance.Services;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BuildingMaintenance
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<AuthenticationService>();
            //services.AddAuthentication("CookieAuth")
            //    .AddCookie("CookieAuth", config =>
            //    {
            //        config.Cookie.Name = "BuildingMaintenance_Auth";
            //        config.LoginPath = "/Authentication/Login";
            //    });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(config =>
                {
                    config.Cookie.Name = "BMaintenanceAuth";
                    config.Cookie.HttpOnly = true;
                    config.LoginPath = new PathString("/Authentication/Login");
                    config.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    config.LogoutPath = new PathString("/Authentication/Logout");
                    //config.AccessDeniedPath = new PathString("/Authentication/AccessDenied");
                    //config.ExpireTimeSpan = TimeSpan.FromSeconds(20);
                });
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60);//You can set Time   
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                //options.Cookie.IsEssential = true;
                //options.Cookie.Name = "BMaintenanceAuth";
            });
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //// who are you?
            //app.UseAuthentication();
            //// are you allowed?
            //app.UseAuthorization();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //    endpoints.MapRazorPages();
            //});


            app.UseEndpoints(endpoints =>
            {
                foreach (var menu in CommonServices.Instance.GetCPSideMenuNavigations())
                {
                    string[] controllerAction = menu.ControllerAction.Split('/');
                    endpoints.MapControllerRoute(
                        name: menu.RouteName,
                        pattern: menu.RouteURL,
                        defaults: new { controller = controllerAction[0], action = controllerAction[1] },
                        dataTokens: new { RouteName = menu.RouteName, CPSideMenuNavigationID = menu.CPSideMenuNavigationID }
                    );

                }
                endpoints.MapControllerRoute(
                    name: "HomePage",
                    pattern: "{controller=society}/{action=index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
