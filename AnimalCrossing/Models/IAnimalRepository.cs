using System;
using System.Collections.Generic;

namespace AnimalCrossing.Models
{
    public interface IAnimalRepository
    {
        public void Save(Cat c);

        public List<Cat> Get();
        public Cat Get(int catId);
        public void Delete(int catId);

        public List<Cat> Find(string search);
    }
}
