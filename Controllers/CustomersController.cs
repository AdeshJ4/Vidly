using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Vidly.Data;
using Vidly.Models;
using Vidly.ViewModels;

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

        public IActionResult New ()
        {

            IEnumerable<MembershipType> membershipType = _db.MembershipType.ToList();

            var viewModel = new NewCustomerViewModel
            {
                MembershipTypes = membershipType
            };

            return View(viewModel);

        }


        [HttpPost]
        public IActionResult Create (Customer customer)
        {
            _db.Customers.Add(customer);
            _db.SaveChanges();  
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id )
        {
            Customer? customer = _db.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();

            var viewModel = new NewCustomerViewModel
            {
                Customer = customer,
                MembershipTypes = _db.MembershipType.ToList()  
            };

            return View("New", viewModel);
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
