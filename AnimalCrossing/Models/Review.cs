using System;
namespace AnimalCrossing.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        public int Rating { get; set; } // 1-5
        public DateTime ReviewDate { get; set; }

        public int ReviewingCatId { get; set; }
        public Cat ReviewingCat { get; set; }

        public string Comment { get; set; }

        public Review()
        {
        }
    }

   
}
