using Vidly.Data;
using Vidly.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Vidly.DTOs;
using System.Drawing.Text;

namespace Vidly.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        
        public CustomersController ( ApplicationDbContext db, IMapper mapper )
        {
            _db = db;
            _mapper = mapper;
        }




 
        // return all customers 
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<CustomerDto>> GetCustomers ()
        {
            //return await _db.Customers.ToListAsync();
            //return Ok(heroes.Select(hero => _mapper.Map<SuperHeroDto>(hero)));
            var res = await _db.Customers.ToListAsync();
            return Ok(res.Select(cs => _mapper.Map<CustomerDto>(cs)));
            //return await _db.Customers.ToListAsync().Select(_mapper.Map<Customer, CustomerDto>);
        }



        // return specific cutomerDto
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer ( [FromRoute] int id )
        {
            Customer? customer = await _db.Customers.SingleOrDefaultAsync(c => c.Id == id);
            if (customer == null)
            {
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                //return NotFound(HttpStatusCode.NotFound);
                //return StatusCode(StatusCodes.Status404NotFound);
                return NotFound();
            }

            //return (IActionResult)cutomerDto;
            //return Ok(res.Select(cs => _mapper.Map<CustomerDto>(cs)));
            return Ok(_mapper.Map<CustomerDto>(customer));
        }



        // create cutomerDto
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer ( CustomerDto cutomerDto )
        {
            if (!ModelState.IsValid)
            {
                //return StatusCode(StatusCodes.Status400BadRequest);
                return BadRequest();
            }

            //SuperHero hero = _mapper.Map<SuperHero>(newHero); // we want to map newHero to SuperHero
            //heroes.Add(hero);
            //return Ok(heroes.Select(hero => _mapper.Map<SuperHeroDto>(hero)));
            // I didn't pass second argunment inside () so it created a new obj and return it inside customer
            Customer customer = _mapper.Map<Customer>(cutomerDto);
            await _db.Customers.AddAsync(customer);
            await _db.SaveChangesAsync();
            cutomerDto.Id = customer.Id;

            return Ok(cutomerDto);
        }




        // update Customer
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<CustomerDto>> UpdateCustomer ( [FromRoute] int id, CustomerDto cutomerDto )
        {
            if (!ModelState.IsValid)
            {
                //return StatusCode(StatusCodes.Status400BadRequest);
                return BadRequest();
            }

            Customer? customerInDb = _db.Customers.SingleOrDefault(c => c.Id == id);
            
            if(customerInDb == null)
            {
                //return StatusCode(StatusCodes.Status404NotFound);
                return NotFound();
            }

            //Customer customer = _mapper.Map<Customer>(cutomerDto);
            //_mapper.Map<Customer>(customerInDb);
            /*
             * Syntax : _mapper.Map<Source, Destination>(source Obj, Destination Obj);
               In previous ex I didn't pass second argunment inside () so it created a new obj and return
               it inside customer.
                but if you have existing object we can pass it here ( ------ , customerInDb) as a 2nd arg.
                _mapper.Map<CustomerDto, Customer>(cutomerDto, customerInDb) --> this line equal to following line
                _mapper.Map(cutomerDto, customerInDb);
                because this generic parameters(<CustomerDto, Customer>) are grayed out beacuse the compiler 
                can infer from the objects we have passed through this method what are the source and target 
                types 
            */
            cutomerDto.Id = customerInDb.Id;
            _mapper.Map<CustomerDto, Customer>(cutomerDto, customerInDb);
            //customerInDb.Name = cutomerDto.Name;  
            //customerInDb.BirthDate = cutomerDto.BirthDate;    
            //customerInDb.IsSubscribedToNewsLetter = cutomerDto.IsSubscribedToNewsLetter;  
            //customerInDb.MemberShipTypeId = cutomerDto.MemberShipTypeId;

            await _db.SaveChangesAsync();

            //return (IActionResult)cutomerDto;
            return Ok(cutomerDto);
        }





        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<CustomerDto>> DeleteContact ( [FromRoute] int id )
        {
            Customer? customerInDb = _db.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
            {
                //return StatusCode(StatusCodes.Status404NotFound);
                return NotFound();  
            }

            _db.Customers.Remove(customerInDb);
            await _db.SaveChangesAsync();

            //return (IActionResult)customerInDb;
            return Ok(customerInDb);

        }
    }
}