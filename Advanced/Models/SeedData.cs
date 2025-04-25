using Microsoft.EntityFrameworkCore;

namespace Advanced.Models
{
    public static class SeedData
    {
        public static void SeedDatabase(DataContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
                context.Database.Migrate();

            if (!context.People.Any()
                && !context.Locations.Any()
                && !context.Departments.Any())
            {
                Department[] departments =
            [
                new() {Name = "Sales"},
                new() {Name = "Development"},
                new() {Name = "Support"},
                new() {Name = "Facilities"},
                ];

                context.Departments.AddRange(departments);
                context.SaveChanges();

                Location[] locations =
                [
                new() {City = "Oakland", State = "California"},
                new() {City = "San Jose", State = "California"},
                new() {City = "New York", State = "New York"},
                ];

                context.Locations.AddRange(locations);
                context.SaveChanges();


                Person[] people =
                [
                new() { FirstName = "John", Surname = "Doe", Department = departments[0], Location = locations[0] },
                new() { FirstName = "Jane", Surname = "Smith", Department = departments[1], Location = locations[1] },
                new() { FirstName = "Alice", Surname = "Johnson", Department = departments[2], Location = locations[2] },
                new() { FirstName = "Bob", Surname = "Brown", Department = departments[0], Location = locations[1] },
                new() { FirstName = "Charlie", Surname = "Davis", Department = departments[1], Location = locations[0] },
                new() { FirstName = "Diana", Surname = "Miller", Department = departments[2], Location = locations[2] },
                new() { FirstName = "Eve", Surname = "Wilson", Department = departments[3], Location = locations[0] },
                new() { FirstName = "Frank", Surname = "Taylor", Department = departments[3], Location = locations[1] },
                new() { FirstName = "Grace", Surname = "Anderson", Department = departments[0], Location = locations[2] }
                ];

                context.People.AddRange(people);
                context.SaveChanges();
            }
        }
    }
}
