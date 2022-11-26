using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class MembershipType
    {
        [Key]
        public byte Id { get; set; }
        public string? Name { get; set; }    
        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }  
        public byte DiscountRate { get; set; }
        
    }
}
