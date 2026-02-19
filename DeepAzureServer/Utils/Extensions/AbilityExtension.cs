using DeepAzureServer.Models.Entities;
using DeepAzureServer.Models.Responses;

namespace DeepAzureServer.Utils.Extensions
{
    public static class AbilityExtension
    {
        public static AbilityResponse ToResponseDto(this Ability ability)
        {
            return new AbilityResponse
            {
                Id = ability.Id,
                Name = ability.Name,
                Description = ability.Description,
            };
        }

        public static ReferenceDto ToReferenceDto(this Ability ability)
        {
            return new ReferenceDto
            {
                Id = ability.Id,
                Name = ability.Name
            };
        }
    }
}
