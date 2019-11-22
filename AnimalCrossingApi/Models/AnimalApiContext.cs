
using Microsoft.EntityFrameworkCore;

namespace AnimalCrossingApi.Models
{
    public class AnimalApiContext : DbContext
    {
        public AnimalApiContext(DbContextOptions<AnimalApiContext> options)
            : base(options)
        {
        }

        public DbSet<AnimalCrossing.Models.Cat> Cats { get; set; }
    }
}