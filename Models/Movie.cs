using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]    
        public string? Name { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [Required]
        // This will work as a foreign key
        public byte GenreId { get; set; }

        // Date movie was added to database.
        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public byte NumberInStock { get; set; }
    }
}
