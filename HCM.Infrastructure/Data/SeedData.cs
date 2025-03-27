using HCM.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace HCM.Infrastructure.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var context = services.GetRequiredService<HcmDbContext>();

            if (context.Users.Any())
            {
                Console.WriteLine("➡️ Database already seeded. Skipping...");
                return;
            }

            Console.WriteLine("🌱 Seeding initial roles and users...");

            // Roles to create
            string[] roles = ["HR Admin", "Manager", "Employee"];

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Seed users
            await CreateUser(userManager, "admin@company.com", "HR Admin", "Admin User");
            await CreateUser(userManager, "manager@company.com", "Manager", "Manager User");
            await CreateUser(userManager, "employee@company.com", "Employee", "Employee User");
        }

        private static async Task CreateUser(UserManager<ApplicationUser> userManager, string email, string role,
            string fullName)
        {
            if (await userManager.FindByEmailAsync(email) != null)
                return;

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = fullName,
                EmailConfirmed = true,
                IsActive = true
            };

            var result = await userManager.CreateAsync(user, "Pass@word1");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
            }
            else
            {
                throw new Exception(
                    $"Failed to create user {email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
    }
}