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
    }
}
