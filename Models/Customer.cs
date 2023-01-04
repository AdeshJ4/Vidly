using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; } 
        
        [Required(ErrorMessage = "Please enter customer's name")]
        [StringLength(100)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [Min18YearsIfAmember]
        public DateTime? BirthDate { get; set; }


        [Display(Name = "Membership Type")]
        public MembershipType? MembershipType { get; set; }


        [Display(Name = "Membership Type Id")]
        public byte MemberShipTypeId { get; set; }

        
    }
}
