using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using WebApp.Models;

namespace WebApp.Controllers
{
    [AutoValidateAntiforgeryToken]
    public sealed class FormController : Controller
    {
        private readonly DataContext _context;

        public FormController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(long id)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name");
            return View("Form", await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.ProductId == id));
        }

        [HttpPost]
        public IActionResult SubmitForm(string name, decimal price)
        {
            TempData["name param"] = name;
            // This is bad.
            // A better option is provided in Chapter 31.
            TempData["price param"] = price.ToString();
            return RedirectToAction(nameof(Results));
        }

        public IActionResult Results()
        {
            return View();
        }
    }
}
