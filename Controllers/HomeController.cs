using Microsoft.AspNetCore.Mvc;

namespace Vidly.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index ()
        {
            return View();
        }
    }
}
