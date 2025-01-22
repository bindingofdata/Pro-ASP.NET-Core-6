using Microsoft.EntityFrameworkCore;

using SportsStore.Models;

namespace SportsStore
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<StoreDbContext>(options =>
                options.UseSqlServer(builder.Configuration["ConnectionStrings:SportsStoreConnection"])
                .UseAsyncSeeding(async (context, _, ct) =>
                {
                    if (context.Set<Product>().AnyAsync().Result)
                    {
                        return;
                    }

                    await context.Set<Product>().AddRangeAsync(
                    [
                        new Product
                        {
                            Name = "Kayak",
                            Description = "A boat for one person",
                            Category = "Watersports",
                            Price = 275
                        },
                        new Product
                        {
                            Name = "Lifejacket",
                            Description = "Protective and fashionable",
                            Category = "Watersports",
                            Price = 48.95m
                        },
                        new Product
                        {
                            Name = "Soccer Ball",
                            Description = "FIFA-approved size and weight",
                            Category = "Soccer",
                            Price = 19.50m
                        },
                        new Product
                        {
                            Name = "Corner Flags",
                            Description = "Give your playing field a professional touch",
                            Category = "Soccer",
                            Price = 34.95m
                        },
                        new Product
                        {
                            Name = "Stadium",
                            Description = "Flat-packed 35,000-seat stadium",
                            Category = "Soccer",
                            Price = 79500
                        },
                        new Product
                        {
                            Name = "Thinking Cap",
                            Description = "Improve brain efficiency by 75%",
                            Category = "Chess",
                            Price = 16
                        },
                        new Product
                        {
                            Name = "Unsteady Chair",
                            Description = "Secretly give your opponent a disadvantage",
                            Category = "Chess",
                            Price = 29.95m
                        },
                        new Product
                        {
                            Name = "Human Chess Board",
                            Description = "A fun game for the family",
                            Category = "Chess",
                            Price = 75
                        },
                        new Product
                        {
                            Name = "Bling-Bling King",
                            Description = "Gold-plated, diamond-studded King",
                            Category = "Chess",
                            Price = 1200
                        }
                    ], ct);
                    await context.SaveChangesAsync(ct);
                }));
            builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();

            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");
            app.UseStaticFiles();
            app.MapDefaultControllerRoute();
            await using (AsyncServiceScope serviceScope = app.Services.CreateAsyncScope())
            await using (StoreDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<StoreDbContext>())
            {
                await RunPendingMigrations(dbContext);
                await dbContext.Database.EnsureCreatedAsync();
            }

            app.Run();
        }

        private static async Task RunPendingMigrations(DbContext dbContext)
        {
            if (dbContext.Database.GetPendingMigrationsAsync().Result.Any())
                await dbContext.Database.MigrateAsync();
        }
    }
}
