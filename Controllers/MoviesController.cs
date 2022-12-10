using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Data;
using Vidly.Migrations;
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

        public IActionResult MovieForm ()
        {
            IEnumerable<Genre> genres = _db.Genre.ToList();

            MovieFormViewModel viewModel = new MovieFormViewModel
            {
                Genre = genres
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save (Movie movie)
        {
            if (!ModelState.IsValid)
            {
                // if ModelState is not valid then i wan't to return same view(the view that include the customer form)
                // viewModel is important to populate the form with the values the user has put int he form
                MovieFormViewModel viewModel = new MovieFormViewModel(movie)
                {
                    Genre = _db.Genre.ToList()
                };
                return View("MovieForm", viewModel);
            }
            // if id == 0 then it is new customer so we should added to database otherwise we should update it.
            if (movie.Id == 0)
                _db.Movies.Add(movie);
            else
            {
                //Now to update an entity we need to get it from database first
                // if the given customer is not found this is going throw an exception
                Movie movieInDb = _db.Movies.Single(c => c.Id == movie.Id);
                //TryUpdateModelAsync(customerInDb);  // don't use this approach beacuse of security reasons
                //AutoMapper.Mapper.Map(customer, customerInDb);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.Genre = movie.Genre;
                movieInDb.NumberInStock = movie.NumberInStock;
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit (int id)
        {
            Movie? movie = _db.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return NotFound();

            MovieFormViewModel viewModel = new MovieFormViewModel(movie)
            {
                Genre = _db.Genre.ToList()
            };

            return View("MovieForm", viewModel);
        }
    }
}