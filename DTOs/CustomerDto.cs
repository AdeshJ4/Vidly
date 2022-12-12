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
        public byte MemberShipTypeId { get; set; }
  
        [DataType(DataType.Date)]
        [Min18YearsIfAmember]
        public DateTime? BirthDate { get; set; }



        [Display(Name = "Membership Type")]
        public MembershipType? MembershipType { get; set; }


        /*
        // comment this line if you are using rest API
        [Display(Name = "Membership Type")]
        public MembershipTypeDto? MembershipTypeDto { get; set; }
         */


        /*
        
        we exclude MembershipType because this is domain class and this proprty here creating dependency from our DTO to our domain model
        so if you change this membership type this can impact our DTO.
        so here we either use primitive data type like integer, string, bytes whatever or custome DTO.
        so if you want to return hierachical structure you wold create another type called membershipType DTOs
        This way your DTOs are completly decoupled from your domain object.
        
        [Display(Name = "Membership Type")]
        public MembershipType? MembershipType { get; set; }

         
         */

    }
}
