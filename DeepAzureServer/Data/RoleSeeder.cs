using DeepAzureServer.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace DeepAzureServer.Data
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<long>>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            string[] roleNames = { "Admin", "User", "GameMaster" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole<long>(roleName));
                }
            }

            string adminEmail = "admin@deepazure.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var newAdmin = new User
                {
                    UserName = "DeepAzureAdmin",
                    Email = adminEmail,
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                // Create user with a hardcoded strong password (change this in Prod!)
                var result = await userManager.CreateAsync(newAdmin, "AdminP@ssw0rd123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                    await userManager.AddToRoleAsync(newAdmin, "GameMaster");
                }
            }
        }
    }
}
