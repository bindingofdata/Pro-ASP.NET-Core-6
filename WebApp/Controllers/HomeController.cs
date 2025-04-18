using Microsoft.AspNetCore.Mvc;

using WebApp.Filters;

namespace WebApp.Controllers
{
    [HttpsOnly]
    public sealed class HomeController : Controller
    {
        //[RequireHttps]
        public IActionResult Index()
        {
            return View("Message", "This is the Index action on the Home controller.");
        }

        //[RequireHttps]
        public IActionResult Secure()
        {
            return View("Message", "This is the Secure action on the Home controller");
        }
    }
}
