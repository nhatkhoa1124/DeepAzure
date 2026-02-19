using System.Linq.Expressions;
using DeepAzureServer.Models.Common;
using DeepAzureServer.Models.Entities;

namespace DeepAzureServer.Repositories.Interfaces
{
    public interface IRepository<T>
        where T : BaseAuditable
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<PagedResult<T>> GetPagedAsync(
            int pageNumber,
            int pageSize,
            params Expression<Func<T, object>>[] includes
        );
        Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task AddAsync(T entity);
        void Update(T entity);
        Task SoftDeleteAsync(int id);
        void HardDelete(T entity);
        Task SaveChangesAsync();
    }
}
