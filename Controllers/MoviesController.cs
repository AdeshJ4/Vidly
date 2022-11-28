using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Data;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
 
        private readonly ApplicationDbContext _db;

        public MoviesController(ApplicationDbContext db)
        {
            _db = db;
        }

        protected override void Dispose ( bool disposing )
        {
            _db.Dispose();
        }

        public IActionResult Index ()
        {
            IEnumerable<Movie> movies = _db.Movies.Include(g => g.Genre).ToList();
            return View(movies);
        }


        public IActionResult Details ( int? id)
        {
            Movie? movie = _db.Movies.Include(m => m.Genre).SingleOrDefault(c=>c.Id == id);
            if (movie == null)
                return NotFound();

            return View(movie);
        }

        
    }
}
