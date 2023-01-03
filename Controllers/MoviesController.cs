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

        public async Task<IActionResult> Index ()
        {
            IEnumerable<Movie> movies = await _db.Movies.Include(g => g.Genre).ToListAsync();
            return View(movies);
        }

        /*
        public async Task<IActionResult> New ()
        {
            var genres = await _db.Genre.ToListAsync();

            var viewModel = new MovieFormViewModel
            {
                Genre = genres
            };

            return View("MovieForm", viewModel);
        }
        */

        public async Task<IActionResult> MovieForm ()
        {
            //IEnumerable<Genre> genres = _db.Genre.ToList();
            var genres = await _db.Genre.ToListAsync();
            var viewModel = new MovieFormViewModel
            {
                Genre = genres
            };

            return View(viewModel);
        }



        public async Task<IActionResult> Edit ( int id )
        {
            Movie? movie = await _db.Movies.SingleOrDefaultAsync(c => c.Id == id);
            if (movie == null)
                return NotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                Genre = await _db.Genre.ToListAsync()
            };

            return View("MovieForm", viewModel);
        }

        public async Task<IActionResult> Details ( int? id )
        {
            Movie? movie = await _db.Movies.Include(m => m.Genre).SingleOrDefaultAsync(c => c.Id == id);
            if (movie == null)
                return NotFound();

            return View(movie);
        }


        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save (Movie movie)
        {
            if (!ModelState.IsValid)
            {
                // if ModelState is not valid then i wan't to return same view(the view that include the customer form)
                // viewModel is important to populate the form with the values the user has put int he form
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genre = _db.Genre.ToList()
                };
                return View("MovieForm", viewModel);
            }
            // if id == 0 then it is new movie so we should added to database otherwise we should update it.
            if (movie.Id == 0)
                _db.Movies.Add(movie);
            else
            {             
                Movie movieInDb = _db.Movies.Single(c => c.Id == movie.Id); 
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;               
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }   



        public async Task<IActionResult> Delete(int id )
        {
            Movie? movie = await _db.Movies.SingleOrDefaultAsync(c => c.Id == id);

            if (movie == null)
                return NotFound();

            _db.Movies.Remove(movie);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
    }
}