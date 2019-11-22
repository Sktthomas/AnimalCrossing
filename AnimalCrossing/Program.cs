using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalCrossing.Data;
using AnimalCrossing.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using AnimalCrossing.DependencyInjection;

namespace AnimalCrossing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    DbInitializer.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            FlyWithWings fly = new FlyWithWings();
            FlyNoWay noFly = new FlyNoWay();

            MallardDuck md = new MallardDuck(fly);
            RedheadDuck red = new RedheadDuck(fly);
            RubberDuck rubberDuck = new RubberDuck(noFly);

            md.Display();
            md.Fly();

            red.Display();
            red.Fly();

            rubberDuck.Display();
            rubberDuck.Fly();



            host.Run();

            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
