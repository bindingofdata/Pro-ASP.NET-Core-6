using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public sealed class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options) { }

        public DbSet<Product> Products => Set<Product>();
    }
}
