using DeepAzureServer.Models.Entities;
using DeepAzureServer.Models.Responses;

namespace DeepAzureServer.Utils.Extensions
{
    public static class ElementExtension
    {
        public static ElementResponse ToResponseDto(this Element element)
        {
            if (element == null)
                return null;

            return new ElementResponse
            {
                Id = element.Id,
                Name = element.Name,
                Description = element.Description,
            };
        }

        public static ReferenceDto ToReferenceDto(this Element element)
        {
            return new ReferenceDto { Id = element.Id, Name = element.Name };
        }
    }
}
