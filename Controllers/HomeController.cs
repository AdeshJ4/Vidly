using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Vidly.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index ()
        {
            return View();
        }
    }
}
