using DeepAzureServer.Models.Entities;
using DeepAzureServer.Models.Responses;

namespace DeepAzureServer.Utils.Extensions
{
    public static class AbilityExtension
    {
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
