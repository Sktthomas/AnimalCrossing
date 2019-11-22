using System;
using System.Collections.Generic;

namespace AnimalCrossing.Models
{
    public interface ISpeciesRepository
    {
        public void Save(Species s);

        public List<Species> Get();
        public Species Get(int speciesId);
        public void Delete(int speciesId);
    }
}
