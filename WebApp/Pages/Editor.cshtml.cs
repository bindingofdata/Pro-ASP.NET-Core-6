using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using WebApp.Models;

namespace WebApp.Pages
{
    public sealed class EditorModel : PageModel
    {
        private readonly DataContext _context;

        public Product Product { get; set; } = new();

        public EditorModel(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task OnGetAsync(long id)
        {
            Product = await _context.Products.FindAsync(id) ?? new();
        }

        public async Task<IActionResult> OnPostAsync(long id, decimal price)
        {
            Product? product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                product.Price = price;
            }
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
