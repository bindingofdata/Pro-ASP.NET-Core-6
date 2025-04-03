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

        public IActionResult List()
        {
            return View(_context.Products);
        }

        // Return different views based on boolean check example
        //public async Task<IActionResult> Index(long id = 1)
        //{
        //    Product? product = await _context.Products.FindAsync(id);
        //    if (product?.CategoryId == 1)
        //        return View("Watersports", product);
        //    else
        //        return View(product);
        //}

        //public IActionResult Common()
        //{
        //    return View();
        //}
    }
}
