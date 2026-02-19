

using DeepAzureServer.Models.Responses;

namespace DeepAzureServer.Services.Interfaces
{
    public interface IElementService
    {
        public Task<IEnumerable<ElementResponse>> GetAllAsync();
        public Task<ElementResponse?> GetByIdAsync(int id);
    }
}
