using System.Text.Json;
using System.Text.Json.Serialization;
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
            if (await context.Elements.AnyAsync())
            {
                return await context.Elements.ToDictionaryAsync(e => e.Name);
            }

            var jsonPath = Path.Combine(AppContext.BaseDirectory, "Data", "Seeds", "elements.json");
            var jsonData = await File.ReadAllTextAsync(jsonPath);
            var elementList = JsonSerializer.Deserialize<List<Element>>(jsonData);
            if (elementList == null)
                return new Dictionary<string, Element>();

            context.Elements.AddRange(elementList);
            await context.SaveChangesAsync();

            return elementList.ToDictionary(e => e.Name);
        }

        public static async Task SeedMatchups(
            AppDbContext context,
            Dictionary<string, Element> elements
        )
        {
            if (await context.ElementMatchups.AnyAsync())
                return;

            var jsonPath = Path.Combine(
                AppContext.BaseDirectory,
                "Data",
                "Seeds",
                "element_matchups.json"
            );
            var jsonData = await File.ReadAllTextAsync(jsonPath);
            var matchupDtos = JsonSerializer.Deserialize<List<MatchupSeedDto>>(jsonData);
            var matchups = new List<ElementMatchup>();

            foreach (var dto in matchupDtos)
            {
                if (
                    elements.TryGetValue(dto.AttackerName, out var attacker)
                    && elements.TryGetValue(dto.DefenderName, out var defender)
                )
                {
                    matchups.Add(
                        new ElementMatchup
                        {
                            AttackerId = attacker.Id,
                            DefenderId = defender.Id,
                            Multiplier = dto.Multiplier,
                        }
                    );
                }
            }

            context.ElementMatchups.AddRange(matchups);
            await context.SaveChangesAsync();
        }

        // Avoid using the whole class for security
        private record MatchupSeedDto(string AttackerName, string DefenderName, float Multiplier);
    }
}
