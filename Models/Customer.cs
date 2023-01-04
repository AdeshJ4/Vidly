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


        /* 
         * It is called "navigation property" because it allow us to navigate from one type to another. 
         * In this case from "Customer to its membership type". \
         * This navigation property is useful when we want to load an object and its related object together from the database.
         * for ex. we can load the customer and its membership type together.
         */

        [Display(Name = "Membership Type")]
        public MembershipType? MembershipType { get; set; }


        /* 
         * Sometimes for optimization we don't want to load the entire membership object we may need only foreign key
         * so we can add another property here 
         * so Entity framework recognize this convention and treats this property as a foreign key
         * MemberShipTypeId is implicitly required.
         */

        [Display(Name = "Membership Type Id")]
        public byte MemberShipTypeId { get; set; }

        
    }
}
