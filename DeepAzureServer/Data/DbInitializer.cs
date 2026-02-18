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

            var elements = await SeedElements(context);
            var abilities = await SeedAbilities(context);

            await SeedMatchups(context, elements);
            await SeedMonsters(context, elements, abilities);
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

        public static async Task SeedMonsters(
            AppDbContext context,
            Dictionary<string, Element> elements,
            Dictionary<string, Ability> abilities
        )
        {
            if (await context.Monsters.AnyAsync())
                return;

            var jsonPath = Path.Combine(AppContext.BaseDirectory, "Data", "Seeds", "monsters.json");
            if (!File.Exists(jsonPath))
                return;

            var jsonData = await File.ReadAllTextAsync(jsonPath);
            var monsterDtos = JsonSerializer.Deserialize<List<MonsterSeedDto>>(jsonData);

            if (monsterDtos == null)
                return;

            var monsters = new List<Monster>();

            foreach (var dto in monsterDtos)
            {
                if (!elements.TryGetValue(dto.PrimaryElementName, out var primElement))
                    continue;

                int? secElementId = null;
                if (
                    !string.IsNullOrEmpty(dto.SecondaryElementName)
                    && elements.TryGetValue(dto.SecondaryElementName, out var secElement)
                )
                {
                    secElementId = secElement.Id;
                }

                int? abilityId = null;
                if (
                    !string.IsNullOrEmpty(dto.AbilityName)
                    && abilities.TryGetValue(dto.AbilityName, out var ability)
                )
                {
                    abilityId = ability.Id;
                }

                var monster = new Monster
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    BaseHealth = dto.BaseHealth,
                    GrowthHealth = dto.GrowthHealth,
                    BaseStrength = dto.BaseStrength,
                    GrowthStrength = dto.GrowthStrength,
                    BaseDefense = dto.BaseDefense,
                    GrowthDefense = dto.GrowthDefense,
                    BaseMagic = dto.BaseMagic,
                    GrowthMagic = dto.GrowthMagic,
                    BaseResistance = dto.BaseResistance,
                    GrowthResistance = dto.GrowthResistance,
                    BaseSpeed = dto.BaseSpeed,
                    GrowthSpeed = dto.GrowthSpeed,
                    Price = dto.Price,
                    PrimaryElementId = primElement.Id,
                    SecondaryElementId = secElementId,
                    AbilityId = abilityId,
                };
                monsters.Add(monster);
            }

            context.Monsters.AddRange(monsters);
            await context.SaveChangesAsync();
        }

        public static async Task<Dictionary<string, Ability>> SeedAbilities(AppDbContext context)
        {
            // If abilities exist, just return them for the lookup map
            if (await context.Abilities.AnyAsync())
            {
                return await context.Abilities.ToDictionaryAsync(a => a.Name);
            }

            var jsonPath = Path.Combine(
                AppContext.BaseDirectory,
                "Data",
                "Seeds",
                "abilities.json"
            );
            if (!File.Exists(jsonPath))
                return new Dictionary<string, Ability>();

            var jsonData = await File.ReadAllTextAsync(jsonPath);
            var abilityDtos = JsonSerializer.Deserialize<List<AbilitySeedDto>>(jsonData);

            if (abilityDtos == null)
                return new Dictionary<string, Ability>();

            var abilities = new List<Ability>();

            foreach (var dto in abilityDtos)
            {
                abilities.Add(
                    new Ability
                    {
                        Name = dto.Name,
                        Description = dto.Description,
                        LogicKey = dto.LogicKey,
                        LogicData = dto.LogicData,
                    }
                );
            }
            context.Abilities.AddRange(abilities);
            await context.SaveChangesAsync();

            return abilities.ToDictionary(a => a.Name);
        }

        // Record DTOs
        private record AbilitySeedDto(
            string Name,
            string Description,
            string LogicKey,
            string LogicData
        );

        private record MatchupSeedDto(string AttackerName, string DefenderName, float Multiplier);

        private record MonsterSeedDto(
            string Name,
            string Description,
            int BaseHealth,
            int GrowthHealth,
            int BaseStrength,
            int GrowthStrength,
            int BaseDefense,
            int GrowthDefense,
            int BaseMagic,
            int GrowthMagic,
            int BaseResistance,
            int GrowthResistance,
            int BaseSpeed,
            int GrowthSpeed,
            int? Price,
            string PrimaryElementName,
            string? SecondaryElementName,
            string? AbilityName
        );
    }
}
