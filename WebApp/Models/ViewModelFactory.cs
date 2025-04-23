namespace WebApp.Models
{
    public static class ViewModelFactory
    {
        public static ProductViewModel Details(Product product)
        {
            return new ProductViewModel
            {
                Product = product,
                Action = "Details",
                ReadOnly = true,
                Theme = "info",
                ShowAction = false,
                Categories = product == null || product.Category == null
                    ? Enumerable.Empty<Category>()
                    : new List<Category> { product.Category },
                Suppliers = product == null || product.Supplier == null
                    ? Enumerable.Empty<Supplier>()
                    : new List<Supplier> { product.Supplier },
            };
        }

        public static ProductViewModel Create(Product product,
            IEnumerable<Category> categories,
            IEnumerable<Supplier> suppliers)
        {
            return new ProductViewModel
            {
                Product = product,
                Categories = categories,
                Suppliers = suppliers,
            };
        }
    }
}
