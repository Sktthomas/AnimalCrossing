using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalCrossing.Models.ViewModels
{
    public class ViewModelCreator
    {
        
        public static AnimalCatVM CreateAnimalCatVm(ISpeciesRepository speciesRepository)
        {
            return new AnimalCatVM()
            {
                Cat = new Cat(),
                SpeciesSelectList = new SelectList(speciesRepository.Get(), "SpeciesId", "Name")
            };
            
        }

        public ViewModelCreator()
        {
        }
    }
}
