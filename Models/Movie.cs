using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Please enter movie name")]
        [MaxLength(250)]    
        public string? Name { get; set; }


        [Display(Name = "Genre Type")]
        public Genre? Genre { get; set; }

 
        [Display(Name = "Genre Id")]
        // This will work as a foreign key
        public byte GenreId { get; set; }


        [Required]
        [Display(Name = "Date Added")]
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }


        [Display(Name= "Release Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }


        [Display(Name = "Number In Stock")]
        [Required]
        [Range(1, 20, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public byte NumberInStock { get; set; }
    }
}
