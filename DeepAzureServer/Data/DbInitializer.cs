using DeepAzureServer.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DeepAzureServer.Data
{
    public static class DbInitializer
    {
        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<
                RoleManager<IdentityRole<long>>
            >();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            await context.Database.MigrateAsync();

            await SeedRolesAsync(roleManager);
            await SeedAdminUserAsync(userManager);

            if (await context.Elements.AnyAsync())
                return;
            var elements = await SeedElements(context);

            await SeedMatchups(context, elements);
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole<long>> roleManager)
        {
            string[] roleNames = { "Admin", "User", "GameMaster" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole<long>(roleName));
                }
            }
        }

        private static async Task SeedAdminUserAsync(UserManager<User> userManager)
        {
            string adminEmail = "admin@deepazure.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var newAdmin = new User
                {
                    UserName = "DeepAzureAdmin",
                    Email = adminEmail,
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                // ⚠️ SECURITY NOTE: In production, read this from Environment Variables!
                var result = await userManager.CreateAsync(newAdmin, "AdminP@ssw0rd123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                    await userManager.AddToRoleAsync(newAdmin, "GameMaster");
                }
            }
        }

        public static async Task<Dictionary<string, Element>> SeedElements(AppDbContext context)
        {
            var elementDict = new Dictionary<string, Element>
            {
                {
                    "Norma",
                    new Element { Name = "Norma", Description = "Normal type." }
                },
                {
                    "Ignis",
                    new Element { Name = "Ignis", Description = "Fire element." }
                },
                {
                    "Aqua",
                    new Element { Name = "Aqua", Description = "Water element." }
                },
                {
                    "Herba",
                    new Element { Name = "Herba", Description = "Grass element." }
                },
                {
                    "Lux",
                    new Element { Name = "Lux", Description = "Light element." }
                },
                {
                    "Tenebrae",
                    new Element { Name = "Tenebrae", Description = "Dark element." }
                },
            };
            context.Elements.AddRange(elementDict.Values);
            await context.SaveChangesAsync();

            return elementDict;
        }

        public static async Task SeedMatchups(
            AppDbContext context,
            Dictionary<string, Element> elements
        )
        {
            var matchups = new List<ElementMatchup>()
            {
                CreateMatchup(elements["Ignis"], elements["Herba"], 2.0f),
                CreateMatchup(elements["Herba"], elements["Aqua"], 2.0f),
                CreateMatchup(elements["Aqua"], elements["Ignis"], 2.0f),
                CreateMatchup(elements["Lux"], elements["Tenebrae"], 1.5f),
                CreateMatchup(elements["Tenebrae"], elements["Lux"], 1.5f),
            };

            context.ElementMatchups.AddRange(matchups);
            await context.SaveChangesAsync();
        }

        // Helpers
        private static ElementMatchup CreateMatchup(
            Element attacker,
            Element defender,
            float multiplier
        )
        {
            return new ElementMatchup
            {
                AttackerId = attacker.Id,
                DefenderId = defender.Id,
                Multiplier = multiplier,
            };
        }
    }
}
