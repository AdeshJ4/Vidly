using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Mvc;
using AllowAnonymousAttribute = Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

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
