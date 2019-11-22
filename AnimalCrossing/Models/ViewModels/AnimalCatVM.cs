using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalCrossing.Models.ViewModels
{
    public class AnimalCatVM
    {
        public Cat Cat { get; set; }
        public SelectList SpeciesSelectList { get; set; }

        public AnimalCatVM()
        {
        }
    }
}
