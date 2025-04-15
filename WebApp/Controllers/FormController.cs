using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using System.Text.Json;

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
        public IActionResult SubmitForm([Bind("Name", "Category")]Product product)
        {
            TempData["name"] = product.Name;
            TempData["price"] = product.Price.ToString();
            TempData["category name"] = product.Category?.Name;
            return RedirectToAction(nameof(Results));
        }

        public IActionResult Results()
        {
            return View();
        }
    }
}
