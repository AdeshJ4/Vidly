using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Data;
using Vidly.Models;
using Vidly.ViewModels;
using AutoMapper;
using Vidly.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        //private readonly ApplicationDbContext _db;
        private readonly VidlyContext _db;
        private readonly IMapper _mapper;

        public CustomersController ( VidlyContext db, IMapper mapper )
        {
            _db = db;
            _mapper = mapper;
        }

        

        protected override void Dispose ( bool disposing )
        {
            _db.Dispose();
        }

        public IActionResult Index ()
        {
           
            return View();
        }
        
   
        public async Task<ActionResult<CustomerDto>> Details ( int? id )
        {
            Customer? customer = await _db.Customers.Include(c => c.MembershipType).SingleOrDefaultAsync(c => c.Id == id); // querry is excuted immediately because of "SingleOrDefault() method"
            if (customer == null)
                return NotFound();

            return View(_mapper.Map<CustomerDto>(customer));
        }

        public async Task<ActionResult<CustomerFormViewModelDTO>> CustomerForm ()  // Creating New User
        {

            IEnumerable<MembershipType> membershipType = await _db.MembershipType.ToListAsync();

            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),  // changes -> default values will be applied like 0 for id
                MembershipTypes = membershipType
            };

            return View(viewModel);
            //return View(_mapper.Map<CustomerFormViewModel, CustomerFormViewModelDTO>(viewModel));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save ( Customer customer )
        {
            if (!ModelState.IsValid)
            {

                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = await _db.MembershipType.ToListAsync()
                };
                
                return View("CustomerForm", viewModel);
            }
           
            if (customer.Id == 0)
                await _db.Customers.AddAsync(customer);
            else
            {

                Customer customerInDb = await _db.Customers.SingleAsync(c => c.Id == customer.Id);

                //AutoMapper.Mapper.Map(customer, customerInDb);
                             
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MemberShipTypeId = customer.MemberShipTypeId;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
             
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }




        public async Task<ActionResult> Edit (int id)
        {
            Customer? customer = await _db.Customers.SingleOrDefaultAsync(c => c.Id == id);
            if (customer == null)
                return NotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = await _db.MembershipType.ToListAsync()  
            };

            return View("CustomerForm", viewModel);
                    
        }


      
        public async Task<IActionResult> Delete ( int id )
        {
            Customer? customer = await _db.Customers.SingleOrDefaultAsync(c => c.Id == id);
            if (customer == null)
                return NotFound();

            
            _db.Customers.Remove(customer);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
