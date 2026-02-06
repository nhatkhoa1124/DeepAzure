using DeepAzureServer.Models.Entities;

namespace DeepAzureServer.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAllAsync();
        public Task<IEnumerable<User>> GetPagedAsync(int pageId, int pageSize);
        public Task<User> GetByIdAsync(int id);
        public Task<User> UpadateByIdAsync(int id);
        public Task<bool> DeleteByIdAsync(int id);
    }
}
