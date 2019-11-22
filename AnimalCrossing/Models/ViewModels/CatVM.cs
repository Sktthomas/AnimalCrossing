using System;
using System.ComponentModel.DataAnnotations;

namespace AnimalCrossing.Models.ViewModels
{
    public class CatVM
    {
        public int CatId { get; set; }


        public string Name { get; set; }

        public string ProfilePicture { get; set; }

        public string Description { get; set; }


        public CatVM()
        {
        }
    }
}
