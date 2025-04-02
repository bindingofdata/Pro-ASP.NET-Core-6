using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public sealed class SecondController : Controller
    {
        public IActionResult Index()
        {
            // this version will match a controller specific view first
            //return View("common");

            // this version will only match the explicitly defined view
            // this version is more brittle, so should be used only when required
            return View("Views/Shared/Common.cshtml");
        }
    }
}
