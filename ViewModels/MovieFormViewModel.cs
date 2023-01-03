using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<Genre> Genre { get; set; }
        //public Movie Movie { get; set; } 


        // Pure View Model
        public int? Id { get; set; }

        [Required(ErrorMessage = "Please enter movie name")]
        [MaxLength(250)]
        public string? Name { get; set; }

        // This will work as a foreign key
        [Display(Name = "Genre")]
        [Required]
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
                return Id == 0 ? "New Movie" : "Edit Movie";
            }
        }


        public MovieFormViewModel ()
        {
            Id = 0;
        }

        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;     
        }
    }
}
