using DeepAzureServer.Models.Common;
using DeepAzureServer.Models.Responses;
using DeepAzureServer.Repositories.Interfaces;
using DeepAzureServer.Services.Interfaces;
using DeepAzureServer.Utils.Extensions;

namespace DeepAzureServer.Services.Implementations
{
    public class MonsterService : IMonsterService
    {
        private readonly IMonsterRepository _monsterRepo;

        public MonsterService(IMonsterRepository monsterRepo)
        {
            _monsterRepo = monsterRepo;
        }

        public async Task<PagedResult<MonsterResponse>> GetPagedAsync(
            int pageNumber = 1,
            int pageSize = 20
        )
        {
            var pagedMonsters = await _monsterRepo.GetPagedAsync(pageNumber, pageSize);
            return pagedMonsters.Map(m => m.ToResponseDto());
        }

        public async Task<MonsterResponse?> GetByIdAsync(int id)
        {
            var monster = await _monsterRepo.GetByIdAsync(id);
            if (monster == null)
                return null;
            return monster.ToResponseDto();
        }
    }
}
