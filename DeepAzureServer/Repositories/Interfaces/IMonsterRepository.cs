using DeepAzureServer.Models.Common;
using DeepAzureServer.Models.Entities;

namespace DeepAzureServer.Repositories.Interfaces
{
    public interface IMonsterRepository
    {
        public Task<PagedResult<Monster>> GetAllAsync(int pageNumber = 1, int pageSize = 20);
        public Task<Monster?> GetByIdAsync();
        public Task DeleteByIdAsync();
    }
}
