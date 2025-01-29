using Microsoft.AspNetCore.Mvc;

using SportsStore.Models;
using SportsStore.Models.ViewModels;

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

        public ViewResult Index(string? category, int productPage = 1) =>
            View(new ProductsListViewModel
            {
                Products = _repository.Products
                .Where(product => string.IsNullOrWhiteSpace(category) ||
                    string.Equals(product.Category, category))
                .OrderBy(product => product.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.Products.Count()
                },
                CurrentCategory = category
            });
    }
}
