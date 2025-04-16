using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using System.Text.Json;

using WebApp.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
                .FirstOrDefaultAsync(p => id == null || p.ProductId == id));
        }

        [HttpPost]
        public IActionResult SubmitForm(Product product)
        {
            bool priceIsValid = ModelState.GetValidationState(nameof(Product.Price))
                    == ModelValidationState.Valid;
            if (priceIsValid && product.Price <= 0)
            {
                ModelState.AddModelError(nameof(product.Price), "Enter a positive price.");
            }
            if (priceIsValid
                && ModelState.GetValidationState(nameof(Product.Name))
                    == ModelValidationState.Valid
                && product.Name.StartsWith("small", StringComparison.OrdinalIgnoreCase)
                && product.Price > 100)
            {
                ModelState.AddModelError("", "Small products cannot cost more than $100.");
            }

            if (ModelState.GetValidationState(nameof(Product.CategoryId))
                    == ModelValidationState.Valid && !_context.Categories.Any(c =>
                        c.CategoryId == product.CategoryId))
            {
                ModelState.AddModelError(nameof(product.CategoryId), "Enter an existing Category Id.");
            }

            if (ModelState.GetValidationState(nameof(Product.SupplierId))
                    == ModelValidationState.Valid && !_context.Suppliers.Any(s =>
                        s.SupplierId == product.SupplierId))
            {
                ModelState.AddModelError(nameof(product.SupplierId), "Enter an existing Supplier Id.");
            }

            if (ModelState.IsValid)
            {
                TempData["name"] = product.Name;
                TempData["price"] = product.Price.ToString();
                TempData["categoryId"] = product.CategoryId.ToString();
                TempData["supplierId"] = product.SupplierId.ToString();
                return RedirectToAction(nameof(Results));
            }
            else
            {
                return View("Form");
            }
        }

        public IActionResult Results()
        {
            return View();
        }
    }
}
