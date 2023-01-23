using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vidly.Data;
using Vidly.DTOs;
using Vidly.Models;
using Rental = Vidly.Models.Rental;
using Microsoft.EntityFrameworkCore;

namespace Vidly.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewRentalsController : ControllerBase
    {

        private readonly VidlyContext _db;
        private readonly IMapper _mapper;

        public NewRentalsController( VidlyContext db, IMapper mapper )
        {
            _db = db;   
            _mapper = mapper;   
        }


        [HttpPost]
        public async Task<ActionResult<NewRentalDto>> CreateNewRentals ( NewRentalDto newRental )
        {
            
            
            var customer = await _db.Customers.SingleAsync(c => c.Id == newRental.CustomerId);

            if (customer == null)
                return BadRequest("CustomerId is not valid");

            var movies = await _db.Movies.Where(m => newRental.MovieIds.Contains(m.Id)).ToListAsync();

            

            foreach(var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available");
                movie.NumberInStock--;
                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now,
                };

                await _db.Rentals.AddAsync(rental);
            }

            await _db.SaveChangesAsync();

            return Ok();
        } 
    }
}
