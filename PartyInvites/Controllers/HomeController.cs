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

        [HttpGet]
        public ViewResult RsvpForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult RsvpForm(GuestResponse guestResponse)
        {
            if (ModelState.IsValid)
            {
                Repository.AddResponse(guestResponse);
                return View("Thanks", guestResponse);
            }
            return View();
        }

        public ViewResult ListResponses()
        {
            return View(Repository.Responses.Where(guest => guest.WillAttend == true));
        }
    }
}
