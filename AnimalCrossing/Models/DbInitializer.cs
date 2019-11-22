using System;
using System.Linq;
using AnimalCrossing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalCrossing.Models
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AnimalCrossingContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AnimalCrossingContext>>()))
            {

                // You add more code here to seed database.
                // See: https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/working-with-sql?view=aspnetcore-3.0&tabs=visual-studio
                if (!context.Species.Any())
                {
                    var species = new Species[] {
                        new Species { Name="Maine Coon", Description="Very big cat..." },
                        new Species { Name="Sphynx", Description="With the right spelling" },
                    };

                    context.Species.AddRange(species);
                    //foreach(var spec in species)
                    //{
                    //    context.Species.Add(spec);
                    //}

                    context.SaveChanges();
                }

                if (!context.Cats.Any())
                {
                    var speciesType = context.Species.ToList();

                    var cats = new Cat[]
                    {
                        new Cat{ Name="Missy", Description="Likes walks on the beach", Gender=Gender.Female, BirthDate= new DateTime(2000, 4, 3),  ProfilePicture="https://www.zooplus.dk/magasin/wp-content/uploads/2018/01/fotolia_138361424-768x658.jpg", SpeciesId= speciesType[0].SpeciesId },
                        new Cat{ Name="Baldy", Description="Likes something ", Gender=Gender.Female, BirthDate= new DateTime(2004, 4, 3),  ProfilePicture="https://images2.minutemediacdn.com/image/upload/c_fill,g_auto,h_1248,w_2220/f_auto,q_auto,w_1100/v1555382975/shape/mentalfloss/sphinxhed.png", SpeciesId=speciesType[1].SpeciesId },
                    };
                    context.Cats.AddRange(cats);

                    
                    context.SaveChanges();
                }


            }


        }
    }
}
