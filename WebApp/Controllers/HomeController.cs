using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public sealed class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("Message", "This is the Index action on the Home controller.");
        }
    }
}
