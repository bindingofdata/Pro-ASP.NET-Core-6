using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WebApp.Models;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class ContentController : ControllerBase
    {
        private DataContext _context;

        public ContentController(DataContext dataContext)
        {
            _context = dataContext;
        }

        [HttpGet("string")]
        public string GetString() => "This is a string response";

        // request format via URL
        [HttpGet("object/{format?}")]
        [FormatFilter]
        // specify Action Result Format(s)
        [Produces("application/json", "application/xml")]
        public async Task<ProductBindingTarget> GetObject()
        {
            Product product = await _context.Products.FirstAsync();
            return new ProductBindingTarget
            {
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                SupplierId = product.SupplierId
            };
        }

        // restrict formats that can be received
        [HttpPost]
        [Consumes("application/json")]
        public string SaveProductJson(ProductBindingTarget product)
        {
            return $"JSON: {product.Name}";
        }

        [HttpPost]
        [Consumes("application/xml")]
        public string SaveProductXml(ProductBindingTarget product)
        {
            return $"XML: {product.Name}";
        }
    }
}
