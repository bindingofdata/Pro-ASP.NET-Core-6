using Microsoft.AspNetCore.Mvc;

using WebApp.Models;

namespace WebApp.Controllers
{
    public sealed class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task<IActionResult> Index(long id = 1)
        {
            return View(await _context.Products.FindAsync(id));
        }
    }
}
