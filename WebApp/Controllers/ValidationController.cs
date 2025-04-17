using Microsoft.AspNetCore.Mvc;

using WebApp.Models;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class ValidationController : ControllerBase
    {
        private readonly DataContext _context;

        public ValidationController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("categorykey")]
        public bool CategoryKey(string? categoryId, [FromQuery] KeyTarget target)
        {
            long keyVal;
            return long.TryParse(categoryId ?? target.CategoryId, out keyVal)
                && _context.Categories.Any(c => c.CategoryId == keyVal);
        }

        [HttpGet("supplierkey")]
        public bool SupplierKey(string? supplierId, [FromQuery] KeyTarget target)
        {
            long keyVal;
            return long.TryParse(supplierId ?? target.SupplierId, out keyVal)
                && _context.Suppliers.Any(s => s.SupplierId == keyVal);
        }
    }

    [Bind(Prefix = "Product")]
    public sealed class KeyTarget
    {
        public string? CategoryId { get; set; }
        public string? SupplierId { get; set; }
    }
}
