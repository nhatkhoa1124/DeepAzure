using DeepAzureServer.Models.Entities;
using DeepAzureServer.Repositories.Interfaces;

namespace DeepAzureServer.Repositories.Implementations
{
    public class ElementRepository : IElementRepository
    {
        public Task DeleteByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Element>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Element?> GetByIdAsync()
        {
            throw new NotImplementedException();
        }
    }
}
