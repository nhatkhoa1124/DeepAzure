using DeepAzureServer.Models.Common;
using DeepAzureServer.Models.Responses;
using DeepAzureServer.Repositories.Interfaces;
using DeepAzureServer.Services.Interfaces;
using DeepAzureServer.Utils.Extensions;

namespace DeepAzureServer.Services.Implementations
{
    public class AbilityService : IAbilityService
    {
        private readonly IAbilityRepository _abilityRepo;

        public AbilityService(IAbilityRepository abilityRepo)
        {
            _abilityRepo = abilityRepo;
        }

        public async Task<AbilityResponse?> GetByIdAsync(int id)
        {
            var result = await _abilityRepo.GetByIdAsync(id);
            if (result == null)
                return null;
            return result.ToResponseDto();
        }

        public async Task<PagedResult<AbilityResponse>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var result = await _abilityRepo.GetPagedAsync(pageNumber, pageSize);
            return result.Map(a => a.ToResponseDto());
        }
    }
}
