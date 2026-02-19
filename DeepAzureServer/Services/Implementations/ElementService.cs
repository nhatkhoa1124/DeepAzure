using DeepAzureServer.Models.Responses;
using DeepAzureServer.Repositories.Interfaces;
using DeepAzureServer.Services.Interfaces;
using DeepAzureServer.Utils.Extensions;

namespace DeepAzureServer.Services.Implementations
{
    public class ElementService : IElementService
    {
        private readonly IElementRepository _elementRepo;

        public ElementService(IElementRepository elementRepo)
        {
            _elementRepo = elementRepo;
        }

        public async Task<IEnumerable<ElementResponse>> GetAllAsync()
        {
            var result = await _elementRepo.GetAllAsync();
            return result.Select(e => e.ToResponseDto()).ToList();
        }

        public async Task<ElementResponse?> GetByIdAsync(int id)
        {
            var result = await _elementRepo.GetByIdAsync(id);
            if (result == null)
                return null;
            return result.ToResponseDto();
        }
    }
}
