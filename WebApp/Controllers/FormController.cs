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

        public async Task<IActionResult> Index(long? id)
        {
            return View("Form", await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id));
        }

        [HttpPost]
        public IActionResult SubmitForm([Bind("Name", "Category")]Product product)
        {
            TempData["name"] = product.Name;
            TempData["price"] = product.Price.ToString();
            TempData["categoryId"] = product.CategoryId.ToString();
            TempData["supplierId"] = product.SupplierId.ToString();
            return RedirectToAction(nameof(Results));
        }

        public IActionResult Results()
        {
            return View();
        }
    }
}
