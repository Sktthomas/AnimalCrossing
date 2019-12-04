using System;
using System.Collections.Generic;
using AnimalCrossing.Models;

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

        internal static List<Cat> GetTestAnimals()
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
    }

}
