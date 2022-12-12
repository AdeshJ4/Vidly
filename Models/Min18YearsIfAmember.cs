using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using Vidly.DTOs;

namespace Vidly.Models
{
    public class Min18YearsIfAmember : ValidationAttribute
    {
        protected override ValidationResult? IsValid ( object? value, ValidationContext validationContext )
        {
            //return base.IsValid(value, validationContext);
            // you can use another proprties of customer class

            Customer customer = (Customer) validationContext.ObjectInstance;  // original
            //CustomerDto customer = (CustomerDto)validationContext.ObjectInstance;

            /* MemberShipTypeId == 1 is id of "pay as you go" and we don't care if it is under 18 or not also
               MemberShipTypeId == 0 when user doen not select a membership type (it will not highlight red border for input field) 
             */
            if (customer.MemberShipTypeId == MembershipType.Unknown || customer.MemberShipTypeId == MembershipType.PayAsYouGo ) 
                return ValidationResult.Success;

            if (customer.BirthDate == null)
                return new ValidationResult("Birthdate is required");

            //we need to calculate age 
            var age = DateTime.Today.Year - customer.BirthDate.Value.Year;

            return (age >= 18) 
                ? ValidationResult.Success 
                : new ValidationResult("Customer should be at least 18 years old to go on membership");
        }
    }
}
