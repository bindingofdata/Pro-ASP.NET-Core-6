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
        public bool CategoryKey(string categoryId)
        {
            long keyVal;
            return long.TryParse(categoryId, out keyVal)
                && _context.Categories.Any(c => c.CategoryId == keyVal);
        }

        [HttpGet("supplierkey")]
        public bool SupplierKey(string supplierId)
        {
            long keyVal;
            return long.TryParse(supplierId, out keyVal)
                && _context.Suppliers.Any(s => s.SupplierId == keyVal);
        }
    }
}
