using Microsoft.AspNetCore.Mvc;

using SportsStore.Models;

namespace SportsStore.Controllers
{
    public sealed class HomeController : Controller
    {
        private readonly IStoreRepository _repository;
        public int PageSize { get; set; } = 4;

        public HomeController(IStoreRepository repo)
        {
            _repository = repo;
        }

        public IActionResult Index(int productPage = 1) =>
            View(_repository.Products
                .OrderBy(product => product.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize));
    }
}
