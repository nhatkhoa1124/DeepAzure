using DeepAzureServer.Models.Entities;
using DeepAzureServer.Repositories.Interfaces;

namespace DeepAzureServer.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        public Task<bool> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetPagedAsync(int pageId, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpadateByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
