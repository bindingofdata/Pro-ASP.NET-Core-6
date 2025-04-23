using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WebApp.Models;

namespace WebApp.Controllers
{
    [AutoValidateAntiforgeryToken]
    public sealed class HomeController : Controller
    {
        private readonly DataContext _dataContext;

        private IEnumerable<Category> _categories => _dataContext.Categories;
        private IEnumerable<Supplier> _suppliers => _dataContext.Suppliers;

        public HomeController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            return View(_dataContext.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier));
        }

        public async Task<IActionResult> Details(long id)
        {
            Product? product = await _dataContext.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.ProductId == id)
                ?? new Product();

            ProductViewModel model = ViewModelFactory.Details(product);
            return View(PRODUCT_EDITOR_STRING, model);
        }

        public IActionResult Create()
        {
            return View(PRODUCT_EDITOR_STRING,
                ViewModelFactory.Create(new Product(), _categories, _suppliers));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(PRODUCT_EDITOR_STRING,
                    ViewModelFactory.Create(product, _categories, _suppliers));
            }

            product.ProductId = default;
            product.Category = default;
            product.Supplier = default;
            _dataContext.Products.Add(product);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            Product? product = await _dataContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ProductViewModel model =
                ViewModelFactory.Edit(product, _categories, _suppliers);

            return View(PRODUCT_EDITOR_STRING, model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(PRODUCT_EDITOR_STRING,
                    ViewModelFactory.Edit(product, _categories, _suppliers));
            }

            product.Category = default;
            product.Supplier = default;
            _dataContext.Products.Update(product);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private const string PRODUCT_EDITOR_STRING = "ProductEditor";
    }
}
