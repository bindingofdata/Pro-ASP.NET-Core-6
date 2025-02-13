using Microsoft.AspNetCore.Identity;
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

            // DB Contexts
            builder.Services.AddDbContext<StoreDbContext>(options =>
                options.UseSqlServer(builder.Configuration["ConnectionStrings:SportsStoreConnection"])
                .UseSeeding((context, _) =>
                {
                    if (context.Set<Product>().Any())
                    {
                        return;
                    }

                    context.Set<Product>().AddRange(GetSeedData());
                    context.SaveChanges();
                })
                .UseAsyncSeeding(async (context, _, ct) =>
                {
                    if (context.Set<Product>().AnyAsync(ct).Result)
                    {
                        return;
                    }

                    await context.Set<Product>().AddRangeAsync(
                    GetSeedData(), ct);
                    await context.SaveChangesAsync(ct);
                }));

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(builder.Configuration["ConnectionStrings:IdentityConnection"])
                );
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            // Scoped services
            builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();
            builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();

            // Additional services
            builder.Services.AddRazorPages();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddScoped<Cart>(provider => SessionCart.GetCart(provider));
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddServerSideBlazor();

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute("catpage",
                "{category}/Page{productPage:int}",
                new { controller = "Home", action = "Index" });

            app.MapControllerRoute("page",
                "Page{productPage:int}",
                new { controller = "Home", action = "Index" });

            app.MapControllerRoute("category",
                "{category}",
                new { controller = "Home", action = "Index", productPage = 1 });

            app.MapControllerRoute("pagination",
                "Products/Page{productPage}",
                new { controller = "Home", action = "Index", productPage = 1 });
            app.MapDefaultControllerRoute();
            app.MapRazorPages();
            app.MapBlazorHub();
            app.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");

            await using (AsyncServiceScope serviceScope = app.Services.CreateAsyncScope())
            await using (StoreDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<StoreDbContext>())
            {
                await dbContext.Database.EnsureCreatedAsync();
                await RunPendingMigrations(dbContext);
            }
            IdentitySeedData.EnsurePopulated(app);

            app.Run();
        }

        private static async Task RunPendingMigrations(DbContext dbContext)
        {
            if (dbContext.Database.GetPendingMigrationsAsync().Result.Any())
                await dbContext.Database.MigrateAsync();
        }

        private static Product[] GetSeedData()
        {
            return
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
            ];
        }
    }
}
