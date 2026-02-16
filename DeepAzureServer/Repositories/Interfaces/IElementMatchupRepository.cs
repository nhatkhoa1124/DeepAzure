using DeepAzureServer.Models.Entities;

namespace DeepAzureServer.Repositories.Interfaces
{
    public interface IElementMatchupRepository
    {
        public Task<IEnumerable<ElementMatchup>> GetAllAsync();
        public Task<ElementMatchup?> GetByIdAsync(int id);
        public Task DeleteByIdAsync(int id);
    }
}
