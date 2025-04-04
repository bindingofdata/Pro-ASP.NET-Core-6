using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using WebApp.Models;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DataContext _context;

        public Product? Product { get; set; }

        public IndexModel(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task OnGetAsync(long id = 1)
        {
            Product = await _context.Products.FindAsync(id);
        }
    }
}
