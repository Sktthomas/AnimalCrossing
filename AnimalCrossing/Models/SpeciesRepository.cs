using System;
using System.Collections.Generic;
using System.Linq;
using AnimalCrossing.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimalCrossing.Models
{
    public class SpeciesRepository : ISpeciesRepository
    {
        private readonly AnimalCrossingContext _context;
        public SpeciesRepository(AnimalCrossingContext context)
        {
            this._context = context;
        }


        public void Delete(int speciesId)
        {
            _context.Species.Remove(this.Get(speciesId));
        }

        public List<Species> Get()
        {
            return _context.Species.ToList();
        }

        public Species Get(int speciesId)
        {
            return _context.Species.Find(speciesId);
        }

        public void Save(Species s)
        {
            if (s.SpeciesId == 0)
            {
                _context.Species.Add(s);
            } else
            {
                _context.Species.Update(s);
            }

            _context.SaveChanges();
        }
    }
}
