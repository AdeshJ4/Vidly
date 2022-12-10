using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<Genre> Genre { get; set; }


        //public Movie Movie { get; set; } 
        // Pure View Model

        [Key]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Please enter movie name")]
        [MaxLength(250)]
        public string? Name { get; set; } 

        [Required]
        [Display(Name = "Genre Id")]
        // This will work as a foreign key
        public byte? GenreId { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Number In Stock")]
        [Required]
        [Range(1, 20, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public byte? NumberInStock { get; set; }


        public string Title
        {
            get
            {
                if (Id != 0)
                    return "Edit Movie";
                return "New Movie";
            }
        }


        public MovieFormViewModel ()
        {
            Id = 0;
        }

        public MovieFormViewModel (Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId; 
        }
    }
}
