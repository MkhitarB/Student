using Microsoft.EntityFrameworkCore;
using Student.DAL;
using Student.Entity.Entities;
using Student.Infrastructure.Enums.EntityEnums;
using Student.Infrastructure.Helpers.Utilities;

namespace Student.Application.Seeds
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(EntityDbContext context)
        {
            // Seed admin user
            await SeedAdminUser(context);

            //Seed classes
            await SeedClasses(context);
        }

        private static async Task SeedAdminUser(EntityDbContext context)
        {
            if (!context.Users.Any(u => u.Email == "admin@gmail.com"))
            {
                var adminUser = new User
                {
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@gmail.com",
                    Address = "2009 W Burbank Blvd Burbank, CA 91506",
                    DateOfBirth = "01/01/2000",
                    FullName = "Admin User",
                    PhoneNumber = "(999) 999-9999",
                    Type = UserType.Admin
                };

                adminUser.Password = Util.HashPassword("adminpassword");

                context.Users.Add(adminUser);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedClasses(EntityDbContext context)
        {
            if (!context.Classes.Any())
            {
                var classes = new List<Class>
            {
                new Class { Name = "Basics Of C#"},
                new Class { Name = "Data Science"},
                new Class { Name = "UI/UX Design"},
                new Class { Name = "Marketing"}
            };

                context.Classes.AddRange(classes);
                await context.SaveChangesAsync();
            }
        }
    }
}
