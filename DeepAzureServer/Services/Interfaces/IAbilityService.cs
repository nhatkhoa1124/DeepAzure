using DeepAzureServer.Models.Common;
using DeepAzureServer.Models.Responses;

namespace DeepAzureServer.Services.Interfaces
{
    public interface IAbilityService
    {
        public Task<PagedResult<AbilityResponse>> GetPagedAsync(int pageNumber, int pageSize);
        public Task<AbilityResponse?> GetByIdAsync(int id);
    }
}
