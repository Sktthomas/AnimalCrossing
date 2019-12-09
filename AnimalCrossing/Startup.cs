using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalCrossing.Data;
using AnimalCrossing.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AnimalCrossing
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
            services.AddControllersWithViews();

            // DI (Dependency Injection) - repository
            services.AddTransient<ISpeciesRepository, SpeciesRepository>();
            services.AddTransient<IAnimalRepository, AnimalRepository>();
            services.AddAuthentication();
            services.AddMvc();
            services.AddDbContext<AnimalCrossingContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("AnimalCrossingContext")));

            //services.AddRazorPages();
            //services.AddMvc().AddRazorPagesOptions(options =>
            //{
            //options.Conventions.AddAreaPageRoute(areaName: "Identity",
            //    pageName: "/Account/Login",
            //    route: "Identity/Account/Login");
            //options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
            //options.Conventions.AuthorizeAreaPage("Admin", "/Index");D
            //options.Conventions.AuthorizeAreaFolder("Admin", "/Users");
            //});




            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.LoginPath = $"/Identity/Account/Login";
            //    options.LogoutPath = $"/Identity/Account/Logout";
            //    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            //});
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

            app.UseAuthentication(); //This might have been what did not let me log in before. Maybe it did not authenticate when not doing this?
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
