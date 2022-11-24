using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Customer
    {
        [Key]
        public int? Id { get; set; } 
        [Required]  
        public string? Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; };
    }
}
