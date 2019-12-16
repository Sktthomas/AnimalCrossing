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
                new Cat{Name="Fat Cat", BirthDate=new DateTime(2019, 9, 23), Description="Fat as fuck lil kitty", Gender=Gender.Male, SpeciesId=speciesType[0].SpeciesId, ProfilePicture="https://pbs.twimg.com/media/DtBuHPeWkAAu9q8.jpg"}, //Notice that you access the Gender enum, then choose your gender within that
                new Cat{Name="Spunky", BirthDate=new DateTime(2019, 9, 23), Description="Got a lotta spunk", Gender=Gender.Male, SpeciesId=speciesType[1].SpeciesId, ProfilePicture="https://upload.wikimedia.org/wikipedia/en/8/87/Keyboard_cat.jpg"}, //Notice the speciesType allows us to access the first (0th) index of species, instead of the SpeciesID
                new Cat{Name="TipTop", BirthDate=new DateTime(2019, 9, 23), Description="Best cat", Gender=Gender.Female, SpeciesId=speciesType[2].SpeciesId, ProfilePicture="https://i.kym-cdn.com/entries/icons/original/000/026/638/cat.jpg"},
                new Cat{Name="Somebody", BirthDate=new DateTime(2019, 9, 23), Description="Just a cat", Gender=Gender.Male, SpeciesId=speciesType[3].SpeciesId, ProfilePicture="https://66.media.tumblr.com/31aca5977137e87fd301d79781c4263c/tumblr_inline_ofu86bnxV21r7k951_540.jpg"},
                new Cat{Name="Pugio", BirthDate=new DateTime(2019, 12, 15), Description="A cat you used to know", Gender=Gender.Other, SpeciesId=speciesType[2].SpeciesId, ProfilePicture="https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/cute-cat-breeds-ragdoll-1568332272.jpg?crop=0.668xw:1.00xh;0.104xw,0&resize=480:*"}
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