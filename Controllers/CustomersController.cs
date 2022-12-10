using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Vidly.Data;
using Vidly.Models;
using Vidly.ViewModels;
using AutoMapper;


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

        public IActionResult CustomerForm ()
        {

            IEnumerable<MembershipType> membershipType = _db.MembershipType.ToList();

            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),  // changes -> default values will be applied like 0 for id
                MembershipTypes = membershipType
            };

            return View(viewModel);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]  
        public IActionResult Save (Customer customer)
        {
            if (!ModelState.IsValid)
            {
                // if ModelState is not valid then i wan't to return same view(the view that include the customer form)
                // viewModel is important to populate the form with the values the user has put int he form
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _db.MembershipType.ToList()
                };
                return View("CustomerForm", viewModel);
            }


            // if id == 0 then it is new customer so we should added to database otherwise we should update it.
            if (customer.Id == 0)
                _db.Customers.Add(customer);
            else
            {
                //Now to update an entity we need to get it from database first
                // if the given customer is not found this is going throw an exception
                Customer customerInDb = _db.Customers.Single(c => c.Id == customer.Id);
                //TryUpdateModelAsync(customerInDb);  // don't use this approach beacuse of security reasons
                //AutoMapper.Mapper.Map(customer, customerInDb);
                customerInDb.Name = customer.Name;  
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MemberShipTypeId = customer.MemberShipTypeId; 
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;  
            }
            _db.SaveChanges();  
            return RedirectToAction("Index");
        }

        
        [HttpPut]
        public IActionResult Edit(int id )
        {
            Customer? customer = _db.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _db.MembershipType.ToList()  
            };

            return View("CustomerForm", viewModel);
        }


        [HttpDelete]
        public IActionResult Delete ( int id )
        {
            Customer? customer = _db.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();

            
            _db.Customers.Remove(customer);
            _db.SaveChangesAsync();
            return View();
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
