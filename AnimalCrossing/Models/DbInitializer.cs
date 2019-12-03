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
                    Microsoft.EntityFrameworkCore.DbContextOptions<AnimalCrossingContext>>()))
            {
                if (!context.Species.Any()) //linq function
                {
                    var species = new Species[]
                 {
                new Species{Name="Tabby", Description="A tabby cat"}, //not 0-indexed so this is id=1
                new Species{Name="Calico", Description="A Calico cat"}, //notice you don't have to call constructor, but instead just add properties
                new Species{Name="Norwegian Forest Cat", Description="A Norwegian Forest cat"}, //You can call constructor if you want though
                new Species{Name="Siamese", Description="A Siamese cat"}
                 };
                    foreach (Species s in species)
                    {
                        context.Species.Add(s);
                    }
                    context.SaveChanges();
                }

                if (!context.Cats.Any())
                {
                    var speciesType = context.Species.ToList(); //Since the primary key is not reset when deleting a database, you have to create a list to be able to robustly pick the right speciesId
                                                                //It is better to do the ToList call here, since every time that function is called, the database is accessed. If you used it while creating the cat, you would end up using a lot of data

                    var cats = new Cat[]
                    {
                new Cat{Name="Fat Cat", BirthDate=new DateTime(2019, 9, 23), Description="Fat as fuck lil kitty", Gender=Gender.Male, SpeciesId=speciesType[0].SpeciesId}, //Notice that you access the Gender enum, then choose your gender within that
                new Cat{Name="Spunky", BirthDate=new DateTime(2019, 9, 23), Description="Got a lotta spunk", Gender=Gender.Male, SpeciesId=speciesType[1].SpeciesId}, //Notice the speciesType allows us to access the first (0th) index of species, instead of the SpeciesID
                new Cat{Name="TipTop", BirthDate=new DateTime(2019, 9, 23), Description="Best cat", Gender=Gender.Female, SpeciesId=speciesType[2].SpeciesId},
                new Cat{Name="Somebody", BirthDate=new DateTime(2019, 9, 23), Description="Fat as fuck lil kitty", Gender=Gender.Male, SpeciesId=speciesType[3].SpeciesId}
                    };
                    foreach (Cat c in cats)
                    {
                        context.Cats.Add(c);

                        context.SaveChanges();
                    }
                }
            }


        }


    }
}