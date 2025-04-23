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
            return View("ProductEditor", model);
        }
    }
}
