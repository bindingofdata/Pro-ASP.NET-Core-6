using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public sealed class ProductBindingTarget
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Range(1,100_000)]
        public decimal Price { get; set; }
        [Range(1, long.MaxValue)]
        public long CategoryId { get; set; }
        [Range(1, long.MaxValue)]
        public long SupplierId { get; set; }

        public Product ToProduct() => new Product
        {
            Name = Name,
            Price = Price,
            CategoryId = CategoryId,
            SupplierId = SupplierId
        };
    }
}
