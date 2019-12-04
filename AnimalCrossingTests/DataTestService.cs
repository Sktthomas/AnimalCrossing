using System;
using System.Collections.Generic;
using AnimalCrossing.Data;
using AnimalCrossing.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalCrossingTests
{
    public class DataTestService
    {
        public static List<Species> GetTestSpecies()
        {
            var sessions = new List<Species>();
            sessions.Add(new Species()
            {
                SpeciesId = 1,
                Name = "Maine coon"
            });
            sessions.Add(new Species()
            {
                SpeciesId = 2,
                Name = "Lynx"
            });
            return sessions;
        }

        public DataTestService()
        {
        }

        public static List<Cat> GetTestAnimals()
        {
            var sessions = new List<Cat>();

            sessions.Add(new Cat()
            {
                CatId = 1,
                Name = "TestKitty1",
                Description = "Test Kitty number 1"
            });
            sessions.Add(new Cat()
            {
                CatId = 1,
                Name = "TestKitty2",
                Description = "Test Kitty number 2"
            });
            return sessions;
        }
        public int Add(int a, int b)
        {
            return a + b;
        }
        public static IAnimalRepository GetInMemoryRepo() //This helper function creates a new AnimalRepository using In-Memory Database which we can use to test repo functions
        {
            DbContextOptions<AnimalCrossingContext> options;
            var builder = new DbContextOptionsBuilder<AnimalCrossingContext>();
            builder.UseInMemoryDatabase("testDB");
            options = builder.Options;
            AnimalCrossingContext animalCrossingContextTest = new AnimalCrossingContext(options);
            animalCrossingContextTest.Database.EnsureDeleted();
            animalCrossingContextTest.Database.EnsureCreated();

            return new AnimalRepository(animalCrossingContextTest);
        }
    }



}
