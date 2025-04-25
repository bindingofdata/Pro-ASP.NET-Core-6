using Advanced.Models;
using Advanced.ViewModels;

using Microsoft.AspNetCore.Mvc;

using System.Data.Entity;

namespace Advanced.Controllers
{
    public sealed class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index([FromQuery] string selectedCity)
        {
            return View(new PeopleListViewModel
            {
                People = _context.People
                    .Include(p => p.Department)
                    .Include(p => p.Location),
                Cities = _context.Locations.Select(l => l.City).Distinct(),
                SelectedCity = selectedCity
            });
        }
    }
}
