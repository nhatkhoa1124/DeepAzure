using DeepAzureServer.Models.Common;
using DeepAzureServer.Models.Responses;

namespace DeepAzureServer.Services.Interfaces
{
    public interface IMonsterService
    {
        public Task<PagedResult<MonsterResponse>> GetPagedAsync(int pageNumber = 1, int pageSize = 20);
        public Task<MonsterResponse?> GetByIdAsync(int id);
    }
}
