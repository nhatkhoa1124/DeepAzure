using DeepAzureServer.Models.Entities;
using DeepAzureServer.Repositories.Interfaces;

namespace DeepAzureServer.Repositories.Implementations
{
    public class ElementMatchupRepository : IElementMatchupRepository
    {
        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ElementMatchup>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ElementMatchup?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
