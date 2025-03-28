using Microsoft.AspNetCore.Mvc;

using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    public sealed class ProductsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return [
                new Product { ProductId = 1, Name = "Product 1" },
                new Product { ProductId = 2, Name = "Product 2" }];
        }

        [HttpGet("{id}")]
        public Product GetProduct()
        {
            return new Product() { ProductId = 1, Name = "Product 1" };
        }
    }
}
