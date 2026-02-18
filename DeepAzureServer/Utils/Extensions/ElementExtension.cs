using DeepAzureServer.Models.Entities;
using DeepAzureServer.Models.Responses;

namespace DeepAzureServer.Utils.Extensions
{
    public static class ElementExtension
    {
        public static ReferenceDto ToReferenceDto(this Element element)
        {
            return new ReferenceDto
            {
                Id = element.Id,
                Name = element.Name
            };
        }
    }
}
