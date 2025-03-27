namespace WebApp.Models
{
    public sealed class Category
    {
        public long CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;

        public IEnumerable<Product>? Products { get; set; }
    }
}
