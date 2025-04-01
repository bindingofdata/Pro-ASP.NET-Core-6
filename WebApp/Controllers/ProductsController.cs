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
        public IAsyncEnumerable<Product> GetProducts()
        {
            return _context.Products.AsAsyncEnumerable();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(long id)
        {
            Product? product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct([FromBody] ProductBindingTarget target)
        {
            if (ModelState.IsValid)
            {
                Product product = target.ToProduct();
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return Ok(product);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task UpdateProduct([FromBody] Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task DeleteProduct(long id)
        {
            _context.Products.Remove(new Product { ProductId = id });
            await _context.SaveChangesAsync();
        }

        [HttpGet("redirect")]
        public IActionResult Redirect()
        {
            // basic redirect
            //return Redirect("/api/products/1");

            // redirect to an Action Method
            // NOTE: does not work with method names that end with "Async"
            // https://www.josephguadagno.net/2020/07/01/no-route-matches-the-supplied-values
            return RedirectToAction(nameof(GetProduct), new { Id = 1 });
        }
    }
}