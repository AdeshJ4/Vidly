using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Data;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CustomersController ( ApplicationDbContext db)
        {
            _db = db;   
        }

        protected override void Dispose ( bool disposing )
        {
            _db.Dispose();
        }



        public IActionResult Index ()
        {
            //IEnumerable<Customer> customers = GetCustomers();
            // get all the customers from the database
            // querry is excuted immediately because of "ToList() method"
            // Read "Eager loading" Note for Include() method.we can't access Membershiptype class object inside View so we have to use Include() method
            IEnumerable<Customer> customers = _db.Customers.Include(c => c.MembershipType).ToList(); 
            return View(customers);
        }

        public IActionResult Details ( int? id )
        {
            //Customer? customer = GetCustomers().SingleOrDefault(c => c.Id == id);
            Customer? customer = _db.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id); // querry is excuted immediately because of "SingleOrDefault() method"
            if (customer == null)
                return NotFound();

            return View(customer);
        }



        /*
         * Following code is Dead Code because we are getting data from database.
       
        private IEnumerable<Customer> GetCustomers ()
        {
            return new List<Customer>
            {
                
                new Customer { Id = 1, Name = "BHARATHI CEMENT CORPORATION PVT. LTD." }
                //new Customer { Id = 2, Name = "Mary Williams" }
            };
        }
        */

    }
}
