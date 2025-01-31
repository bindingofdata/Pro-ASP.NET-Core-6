namespace SportsStore.Models
{
    public sealed class Cart
    {
        public List<CartLine> Lines { get; set; } = [];

        public void AddItem(Product product, int quantity)
        {
            CartLine? line = Lines
                .Where(cartLine => cartLine.Product.ProductID == product.ProductID)
                .FirstOrDefault();

            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Product product) =>
            Lines.RemoveAll(cartLine => cartLine.Product.ProductID == product.ProductID);

        public decimal ComputeTotalValue() =>
            Lines.Sum(cartLine => cartLine.Product.Price * cartLine.Quantity);

        public void Clear() => Lines.Clear();
    }

    public sealed class CartLine
    {
        public int CartLineId { get; set; }
        public Product Product { get; set; } = new();
        public int Quantity { get; set; }
    }
}
