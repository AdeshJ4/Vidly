using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.DTOs
{
    public class CustomerDto
    {
        [Key]     
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter customer's name")]
        [StringLength(100)]
        public string Name { get; set; }

       
        public bool IsSubscribedToNewsLetter { get; set; }
  
        [DataType(DataType.Date)]
        [Min18YearsIfAmember]
        public DateTime? BirthDate { get; set; }



        [Display(Name = "Membership Type")]
        public MembershipTypeDto? MembershipType { get; set; }

        public byte MemberShipTypeId { get; set; }

    }
}
