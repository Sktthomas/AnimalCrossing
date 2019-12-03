using Microsoft.EntityFrameworkCore;
using AnimalCrossing.Models;
using System;

namespace AnimalCrossing.Data
{
    public class AnimalCrossingContext : DbContext //The colon means that it inherits the class after it. 
    {
        public AnimalCrossingContext(Microsoft.EntityFrameworkCore.DbContextOptions<AnimalCrossingContext> options)
            : base(options) //this is the same thing as writing super(options); in Java
        {
        }

        public DbSet<Cat> Cats { get; set; }
        public DbSet<Species> Species { get; set; }

    }
}
