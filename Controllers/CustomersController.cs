using Microsoft.AspNetCore.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        public IActionResult Index ()
        {
            IEnumerable<Customer> customers = GetCustomers();

            return View(customers);
        }

        public IActionResult Details ( int? id )
        {
            Customer? customer = GetCustomers().SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        private IEnumerable<Customer> GetCustomers ()
        {
            return new List<Customer>
            {
                
                new Customer { Id = 1, Name = "BHARATHI CEMENT CORPORATION PVT. LTD." }
                //new Customer { Id = 2, Name = "Mary Williams" }
            };
        }
    }
}
