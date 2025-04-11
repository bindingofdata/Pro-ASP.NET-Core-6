using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace WebApp.Models
{
    public sealed class Product
    {
        public long ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "decimal(8, 2)")]
        [DisplayFormat(DataFormatString = "{0:c2}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        public long CategoryId { get; set; }

        public Category? Category { get; set; }

        public long SupplierId { get; set; }
        // prevent specific property from being serialized when null
        //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Supplier? Supplier { get; set; }
    }
}
