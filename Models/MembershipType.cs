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

        /* i make it readonly so i don't accidently change it somewhere else in the code. so once we initialize it here if 
         * we try to change it somewhere else in the code we will get compile time error
         * Unknown is combination of -> Monthly, Quartel, Annual
        */
        public static readonly byte Unknown = 0;     // 
        public static readonly byte PayAsYouGo = 1;  // here 1 is MembershipTypeId

    }
}
