using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Customer
    {
        [Key]
        public int? Id { get; set; } 
        [Required]  
        public string? Name { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }

        /* 
         * It is called "navigation property" because it allow us to navigate from one type to another. 
         * In this case from "Customer to its membership type". \
         * This navigation property is useful when we want to load an object and its related object together from the database.
         * for ex. we can load the customer and its membership type together.
         */
        public MembershipType? MembershipType { get; set; }


        /* 
         * Sometimes for optimization we don't want to load the entire membership object we may need only foreign key
         * so we can add another property here 
         * so Entity framework recognize this conventionand treats this property as aforeign key
         */

        public byte MemberShipTypeId { get; set; }  
    }
}
