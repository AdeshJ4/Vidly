using Microsoft.AspNetCore.Mvc.Rendering;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class CustomerFormViewModel
    {
        //public List<SelectListItem>? MembershipTypes { get; set; }
        public IEnumerable<MembershipType>? MembershipTypes { get; set; }  
        public Customer Customer { get; set; }  
    }
}
