
namespace SportsStore.Models
{
    public sealed class EFStoreRepository : IStoreRepository
    {
        private StoreDbContext _context;

        public EFStoreRepository(StoreDbContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<Product> Products => _context.Products;
    }
}
