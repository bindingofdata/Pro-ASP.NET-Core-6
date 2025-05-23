﻿using Microsoft.AspNetCore.Mvc.RazorPages;

using WebApp.Models;

namespace WebApp.Pages
{
    public class EditorPageModel : PageModel
    {
        public DataContext DataContext { get; set; }

        public IEnumerable<Category> Categories => DataContext.Categories;
        public IEnumerable<Supplier> Suppliers => DataContext.Suppliers;

        public ProductViewModel? ViewModel { get; set; }

        public EditorPageModel(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        protected async Task CheckNewCategory(Product product)
        {
            if (product.CategoryId == -1
                    && !string.IsNullOrWhiteSpace(product.Category?.Name))
            {
                DataContext.Categories.Add(product.Category);
                await DataContext.SaveChangesAsync();
                product.CategoryId = product.Category.CategoryId;
                ModelState.Clear();
                TryValidateModel(product);
            }
        }
    }
}
