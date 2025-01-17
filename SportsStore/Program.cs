using Microsoft.EntityFrameworkCore;

using SportsStore.Models;

namespace SportsStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<StoreDbContext>(options =>
                options.UseSqlServer(builder.Configuration["ConnectionStrings:SportsStoreConnection"]));
            builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();

            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");
            app.UseStaticFiles();
            app.MapDefaultControllerRoute();
            StoreDbContext dbContext = (app as IApplicationBuilder)
                .ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<StoreDbContext>();
            RunPendingMigrations(dbContext);
            SeedData.EnsurePopulated(dbContext);

            app.Run();
        }

        private static void RunPendingMigrations(DbContext dbContext)
        {
            if (dbContext.Database.GetPendingMigrations().Any())
                dbContext.Database.Migrate();
        }
    }
}
