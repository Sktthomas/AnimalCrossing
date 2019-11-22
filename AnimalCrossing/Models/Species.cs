using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnimalCrossing.Models
{
    public class Species
    {
        public int SpeciesId { get; set; }
        [Required] public string Name { get; set; }
        public string Description { get; set; }

        public List<Cat> Cats { get; set; }
            

        public Species()
        {
        }
    }
}
