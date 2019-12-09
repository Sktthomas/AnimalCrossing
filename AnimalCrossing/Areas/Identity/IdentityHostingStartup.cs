using System;
using AnimalCrossing.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(AnimalCrossing.Areas.Identity.IdentityHostingStartup))]
namespace AnimalCrossing.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AnimalCrossingIdentityDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AnimalCrossingContext")));

                services.AddDefaultIdentity<IdentityUser>(x => {
                    x.SignIn.RequireConfirmedAccount = false;
                    x.SignIn.RequireConfirmedEmail = false;
                    x.SignIn.RequireConfirmedPhoneNumber = false;

                    x.Password.RequireDigit = false;
                    x.Password.RequireNonAlphanumeric = false;
                    x.Password.RequiredLength = 8;

                }).AddEntityFrameworkStores<AnimalCrossingIdentityDbContext>();

            });
        } 

    }
}