using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public sealed class IdentitySeedData
    {
        private const string _adminUserName = "admin";
        // This should not be here.
        // A later chapter covers excluding sensitive data from source control.
        // In either case, the password should be changed once the initial admin account is created.
        private const string _adminPassword = "Secret123$";
        private const string _adminEmail = "admin@fake.com";
        private const string _adminPhone = "555-555-1234";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            AppIdentityDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<AppIdentityDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            UserManager<IdentityUser> userManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser? user = await userManager.FindByNameAsync(_adminUserName);
            if (user == null)
            {
                user = new IdentityUser(_adminUserName);
                user.Email = _adminEmail;
                user.PhoneNumber = _adminPhone;
                await userManager.CreateAsync(user, _adminPassword);
            }
        }
    }
}
