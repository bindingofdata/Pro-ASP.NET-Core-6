using Microsoft.AspNetCore.Mvc;

using WebApp.Models;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class SuppliersController : ControllerBase
    {
        private readonly DataContext _context;

        public SuppliersController(DataContext dataContext)
        {
            _context = dataContext;
        }

        [HttpGet("{id}")]
        public async Task<Supplier?> GetSupplier(long id)
        {
            return await _context.Suppliers.FindAsync(id);
        }
    }
}
