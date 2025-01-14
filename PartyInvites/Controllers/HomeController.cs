using Microsoft.AspNetCore.Mvc;

using PartyInvites.Models;

using System.Diagnostics;

namespace PartyInvites.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
