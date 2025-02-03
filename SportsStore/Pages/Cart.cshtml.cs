using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using SportsStore.Infrastructure;
using SportsStore.Models;

namespace SportsStore.Pages
{
    public class CartModel : PageModel
    {
        private IStoreRepository _repository;

        public CartModel(IStoreRepository repository, Cart cart)
        {
            Cart = cart;
            _repository = repository;
        }

        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        public IActionResult OnPost(long productId, string returnUrl)
        {
            Product? product = _repository.Products
                .FirstOrDefault(product => product.ProductID == productId);
            if (product != null)
            {
                Cart.AddItem(product, 1);
            }
            return RedirectToPage(new { returnUrl });
        }

        public IActionResult OnPostRemove(long productId, string returnUrl)
        {
            Product? product = _repository.Products
                .FirstOrDefault(product => product.ProductID == productId);
            if (product != null)
            {
                Cart.RemoveLine(product);
            }
            return RedirectToPage(new { returnUrl });
        }
    }
}
