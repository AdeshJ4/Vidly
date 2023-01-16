using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Areas.Identity.Data;

// Add profile data for application users by adding properties to the VidlyUser class
public class VidlyUser : IdentityUser
{
    public string? Name { get; set; }
    
    [Required]
    [StringLength(255)] 
    public string? DrivingLicense { get; set; }

}

