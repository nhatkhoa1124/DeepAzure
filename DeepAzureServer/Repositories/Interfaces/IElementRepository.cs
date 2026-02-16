using DeepAzureServer.Models.Entities;

namespace DeepAzureServer.Repositories.Interfaces
{
    public interface IElementRepository
    {
        public Task<IEnumerable<Element>> GetAllAsync();
        public Task<Element?> GetByIdAsync();
        public Task DeleteByIdAsync();
    }
}
