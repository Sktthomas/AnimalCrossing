using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnimalCrossing.Models
{
    public class Cat
    {
        // C# properties = java attribute and public get and set.
        //private int JavaCatId;

        //public int getJavaCatId()
        //{
        //    return this.JavaCatId;
        //}
        //public void setJavaCatId(int catId)
        //{
        //    this.JavaCatId = catId;
        //}


        //if the name is .e.g. CatId, entity framework will pick it up and use it as primary key, convention over configuration (it follows the rules setup)


        public int CatId { get; set; }


        [Required(ErrorMessage = "All cats must have a name to be a pet")] //Using ErrorMessage displays a custom error message
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "No special characters allowed. First character must be uppercase"), MaxLength(30, ErrorMessage = "Your cat's name is too long"), MinLength(3, ErrorMessage = "Your cat's name is too short")]
        public string Name { get; set; }

        // Later, create 1-to-many relationship to Species table - Done!
        //public string Species { get; set; }


        public Gender? Gender { get; set; }

        [Display(Name = "Birth Date"), DataType(DataType.Date)] //Change the displayed unit to Birth Date instead of BirthDate
        public DateTime? BirthDate { get; set; }



        public string? ProfilePicture { get; set; }

        [Required(ErrorMessage = "Please write something about your cat"), StringLength(100, ErrorMessage = "No more than 100 characters allowed")]
        public string Description { get; set; }

        // Ratings..Comments, Reviews
        // 

        public int SpeciesId { get; set; }
        public Species Species { get; set; }

        public List<Review> Reviews { get; set; }

        public Cat()
        {
        }



    }

    public enum Gender { Male, Female, Other };
}
