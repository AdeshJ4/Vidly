using System.ComponentModel.DataAnnotations;

namespace Vidly.DTOs
{
    public class GenreDto
    {
        [Key]
        public byte Id { get; set; }
        public string Name { get; set; }    
    }
}
