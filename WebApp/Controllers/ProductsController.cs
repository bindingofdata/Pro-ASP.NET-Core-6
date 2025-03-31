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
        public IAsyncEnumerable<Product> GetProductsAsync()
        {
            return _context.Products.AsAsyncEnumerable();
        }

        [HttpGet("{id}")]
        public async Task<Product?> GetProductAsync(long id)
        {
            return await _context.Products.FindAsync(id);
        }

        [HttpPost]
        public async Task SaveProductAsync([FromBody] ProductBindingTarget target)
        {
            await _context.Products.AddAsync(target.ToProduct());
            await _context.SaveChangesAsync();
        }

        [HttpPut]
        public async Task UpdateProductAsync([FromBody] Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task DeleteProductAsync(long id)
        {
            _context.Products.Remove(new Product { ProductId = id });
            await _context.SaveChangesAsync();
        }
    }
}
