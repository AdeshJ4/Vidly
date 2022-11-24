using Microsoft.AspNetCore.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
 
        public IActionResult Index ()
        {
            IEnumerable<Movie> movies = GetMovies();

            return View(movies);
        }

        private IEnumerable<Movie> GetMovies ()
        {
            return new List<Movie>
            {
                new Movie { Id = 1, Name = "Shrek" },
                new Movie { Id = 2, Name = "Wall-e" }
            };
        }















        [Route("movies/released/{year:int?}/{month:range(1,12)?}")]
        public IActionResult ByReleaseDate(int? year, int? month)
        {
            return Content(year + "/" + month);
        }
        public IActionResult Random()
        {
            Movie movie = new Movie() { Name="Pirets of caribian"};
            List<Customer> customers = new List<Customer> 
            { 
                 new Customer(){ Name = "Adesh"},
                 new Customer(){ Name = "Akshay"}
            };

            // create ViewModel object
            RandomMovieViewModel viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
            //return Content("Hello World");
            //return NotFound();
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name"});
        }

        //setting default vales
        public IActionResult DefaultIndex(int? pageIndex, string sortBy)
        {
            if (pageIndex == null)
                pageIndex = 1;
            if (sortBy == null)
                sortBy = "Name";
            return Content(string.Format("pageIndex : {0} \n sortby : {1}", pageIndex, sortBy));
        }


        public IActionResult Edit(int? id)
        {
            return Content("id= " + id);
        }

        
    }
}
