using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.DTOs
{
    public class MovieDto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter movie name")]
        [MaxLength(250)]
        public string Name { get; set; }


        [Display(Name = "Genre Type")]
        public GenreDto? Genre { get; set; }


        public byte GenreId { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Range(1, 20)]
        public byte NumberInStock { get; set; }
    }
}
