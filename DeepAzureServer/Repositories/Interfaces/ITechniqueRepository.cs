using DeepAzureServer.Models.Common;
using DeepAzureServer.Models.Entities;

namespace DeepAzureServer.Repositories.Interfaces
{
    public interface ITechniqueRepository
    {
        public Task<PagedResult<Technique>> GetAllAsync(int pageId = 1, int pageSize = 20);
        public Task<Technique?> GetByIdAsync(int id);
        public Task DeleteByIdAsync(int id);
    }
}
