using Microsoft.EntityFrameworkCore;

namespace Platform.Models
{
    public sealed class SeedData
    {
        private readonly CalculationContext _context;
        private readonly ILogger<SeedData> _logger;

        private static readonly Dictionary<int, long> _data = new Dictionary<int, long>()
        {
            { 1,1 }, { 2,3 }, { 3, 6 }, { 4,10 }, { 5,15 },
            { 6,21 }, { 7,28 }, { 8,36 }, { 9,45 }, { 10,55 }
        };

        public SeedData(CalculationContext context, ILogger<SeedData> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void SeedDatabase()
        {
            if (_context.Database.GetPendingMigrations().Any())
            {
                _context.Database.Migrate();
            }

            if (!_context.Calculations.Any())
            {
                _logger.LogInformation("Preparing to seed database");
                _context.Calculations.AddRange(
                    _data.Select(kvp =>
                        new Calculation() { Count = kvp.Key, Result = kvp.Value}));
                _context.SaveChanges();
                _logger.LogInformation("Database seeded");
            }
            else
            {
                _logger.LogInformation("Database already seeded");
            }
        }
    }
}
