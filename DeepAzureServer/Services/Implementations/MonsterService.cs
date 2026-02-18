using DeepAzureServer.Models.Common;
using DeepAzureServer.Models.Entities;
using DeepAzureServer.Repositories.Interfaces;
using DeepAzureServer.Services.Interfaces;

namespace DeepAzureServer.Services.Implementations
{
    public class MonsterService : IMonsterService
    {
        private readonly IMonsterRepository _monsterRepo;

        public MonsterService(IMonsterRepository monsterRepo)
        {
            _monsterRepo = monsterRepo;
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _monsterRepo.DeleteByIdAsync(id);
        }

        public async Task<PagedResult<Monster>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            return await _monsterRepo.GetAllAsync(pageNumber, pageSize);
        }

        public async Task<Monster?> GetByIdAsync(int id)
        {
            return await _monsterRepo.GetByIdAsync(id);
        }
    }
}
