using System;
using System.Collections.Generic;
using System.Linq;
using AnimalCrossing.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimalCrossing.Models
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly AnimalCrossingContext _context;
        public AnimalRepository(AnimalCrossingContext _context)
        {
            this._context = _context;
        }

        public void Delete(int catId)
        {
            _context.Cats.Remove(this.Get(catId));  //You need to find the entire object to delete it
            _context.SaveChanges(); //Remember to always save changes after manipulating database
        }

        public List<Cat> Find(string searchString)
        {
            var cats = from m in _context.Cats.Include(cat => cat.Species) //if you want to include a species in the LINQ query you can use the include method
                       select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                cats = cats.Where(cat =>
                cat.Name.Contains(searchString)
                || cat.Species.Name.Contains(searchString)); //This search for whether the search string contains the name of a species
            }

            return cats.ToList();  //instead of putting it after the LINQ statement, since that would create several calls to the database context

        }

        public List<Cat> Get()
        {
            return _context.Cats.ToList();
        }

        public Cat Get(int catId)
        {
            return _context.Cats.Find(catId);
        }

        public void Save(Cat c)
        {
            if (c.CatId == 0) //int is primitive and therefore never null 
            {
                _context.Cats.Add(c);
            }
            else
            {
                _context.Cats.Update(c); //If the SpeciesId is not 0 it is considered initialized and inside the DB, therefore we need to update it, not save a new instance
            }

            _context.SaveChanges();
        }
    }
}
