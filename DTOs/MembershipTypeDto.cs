using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.DTOs
{
    public class MembershipTypeDto
    {
        [Key]
        public byte Id { get; set; }
        public string Name { get; set; }

    }
}
