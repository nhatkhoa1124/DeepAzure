using DeepAzureServer.Models.Entities;
using DeepAzureServer.Models.Responses;

namespace DeepAzureServer.Utils.Extensions
{
    public static class MonsterExtension
    {
        public static MonsterResponse ToResponseDto(this Monster monster)
        {
            return new MonsterResponse
            {
                Id = monster.Id,
                Name =monster.Name,
                Description = monster.Description,
                BaseHealth = monster.BaseHealth,
                BaseStrength = monster.BaseStrength,
                BaseDefense = monster.BaseDefense,
                BaseMagic = monster.BaseMagic,
                BaseResistance = monster.BaseResistance,
                BaseSpeed = monster.BaseSpeed,
                Price = monster.Price,
                PrimaryElement = monster.PrimaryElement!.ToReferenceDto(),
                SecondaryElement = monster.SecondaryElement?.ToReferenceDto(),
                Ability = monster.Ability?.ToReferenceDto()
            };
        }

        public static ReferenceDto ToReferenceDto(this Monster monster)
        {
            return new ReferenceDto
            {
                Id = monster.Id,
                Name = monster.Name
            };
        }
    }
}
