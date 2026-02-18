using DeepAzureServer.Models.Common;
using DeepAzureServer.Models.Entities;

namespace DeepAzureServer.Services.Interfaces
{
    public interface IMonsterService
    {
        public Task<PagedResult<Monster>> GetAllAsync(int pageNumber = 1, int pageSize = 20);
        public Task<Monster?> GetByIdAsync(int id);
        public Task DeleteByIdAsync(int id);
    }
}
