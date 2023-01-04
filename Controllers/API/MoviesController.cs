using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Data;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public MoviesController ( ApplicationDbContext db, IMapper mapper )
        {
            _db = db;
            _mapper = mapper;
        }

        //return all movies
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<MovieDto>> GetMovies ()
        {
            var res = await _db.Movies.Include(m=> m.Genre).ToListAsync();
            return Ok(res.Select(cs => _mapper.Map<MovieDto>(cs)));
        }

        // return specific movie
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<MovieDto>> GetMovie ( [FromRoute] int id )
        {
            Movie? movie = await _db.Movies.Include(m => m.Genre).SingleOrDefaultAsync(c => c.Id == id);
            if (movie == null)
                return NotFound();
            return Ok(_mapper.Map<MovieDto>(movie));
        }

        // Create Movie 
        [HttpPost]
        public async Task<ActionResult<MovieDto>> CreateMovie ( MovieDto movieDto )
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //Movie movie = _mapper.Map<Movie>(movieDto);
            var movieInDB = _mapper.Map<MovieDto, Movie>(movieDto);
            await _db.Movies.AddAsync(movieInDB);
            await _db.SaveChangesAsync();
            movieDto.Id = movieInDB.Id;
            return Ok(movieDto);
            //return Created(new Uri(Request.R + "/" + movieInDB.Id), movieDto);
        }


        // update Movie
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<MovieDto>> UpdateMovie ( [FromRoute] int id, MovieDto movieDto )
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Movie? movie = await _db.Movies.SingleOrDefaultAsync(c => c.Id == id);

            if (movie == null)
                return NotFound();

            //movieDto.Id = movie.Id;
            _mapper.Map<MovieDto, Movie>(movieDto, movie);
            await _db.SaveChangesAsync();
            return Ok(movieDto);
        }



        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<MovieDto>> DeleteMovie ( [FromRoute] int id )
        {
            Movie? movie = await _db.Movies.Include(m=> m.Genre).SingleOrDefaultAsync(c => c.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            _db.Movies.Remove(movie);
            await _db.SaveChangesAsync();
            
            return Ok(movie);
        }
    }
}
