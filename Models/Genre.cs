
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Genre
    {
        [Key]
        public byte Id { get; set; }

        [StringLength(100)]  
        public string Name { get; set; }    
    }
}
