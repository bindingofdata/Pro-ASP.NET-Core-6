using Microsoft.AspNetCore.Mvc;

using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    public sealed class ProductsController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductsController(DataContext dataContext)
        {
            _context = dataContext;
        }

        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return _context.Products;
        }

        [HttpGet("{id}")]
        public Product? GetProduct(long id, [FromServices]ILogger<ProductsController> logger)
        {
            logger.LogDebug("GetProduct Action Invoked");
            return _context.Products.Find(id);
        }
    }
}
