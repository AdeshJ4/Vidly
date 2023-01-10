using Vidly.Data;
using Vidly.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Vidly.DTOs;

namespace Vidly.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        //private readonly ApplicationDbContext _db;
        private readonly VidlyContext _db;
        private readonly IMapper _mapper;
        
        public CustomersController ( VidlyContext db, IMapper mapper )
        {
            _db = db;
            _mapper = mapper;
        }

        // return all customers 
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<CustomerDto>> GetCustomers ()
        {           
            var res = await _db.Customers.Include(c => c.MembershipType).ToListAsync();
            return Ok(res.Select(cs => _mapper.Map<CustomerDto>(cs)));
        }



        // return specific cutomer
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer ( [FromRoute] int id )
        {
            Customer? customer = await _db.Customers.Include(c => c.MembershipType).SingleOrDefaultAsync(c => c.Id == id);
            if (customer == null)
                return NotFound();

            return Ok(_mapper.Map<CustomerDto>(customer));
        }



        // create cutomer
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer ( CustomerDto cutomerDto )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //Customer customer = _mapper.Map<Customer>(cutomerDto);
            //cutomerDto.Id = customer.Id;
            Customer customer = _mapper.Map<CustomerDto, Customer>(cutomerDto);
            await _db.Customers.AddAsync(customer);
            await _db.SaveChangesAsync();
            return Ok(cutomerDto);
        }




        // update Customer
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<CustomerDto>> UpdateCustomer ( [FromRoute] int id, CustomerDto cutomerDto )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Customer? customer = await _db.Customers.SingleOrDefaultAsync(c => c.Id == id);
            
            if(customer == null)
            {
                return NotFound();
            }
            
            //Syntax : _mapper.Map<Source, Destination>(source Obj, Destination Obj);
            //cutomerDto.Id = customerInDb.Id;
            _mapper.Map<CustomerDto, Customer>(cutomerDto, customer);

            await _db.SaveChangesAsync();

            return Ok(cutomerDto);
        }





        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<CustomerDto>> DeleteCustomer ( [FromRoute] int id )
        {
            Customer? customerInDb = await _db.Customers.SingleOrDefaultAsync(c => c.Id == id);

            if (customerInDb == null)
            {
                //return StatusCode(StatusCodes.Status404NotFound);
                return NotFound();  
            }

            _db.Customers.Remove(customerInDb);
            await _db.SaveChangesAsync();
            
            return Ok(customerInDb);
        }
    }
}