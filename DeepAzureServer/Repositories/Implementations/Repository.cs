using System.Linq.Expressions;
using DeepAzureServer.Data;
using DeepAzureServer.Models.Common;
using DeepAzureServer.Models.Entities;
using DeepAzureServer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeepAzureServer.Repositories.Implementations
{
    public class Repository<T> : IRepository<T>
        where T : BaseAuditable
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<PagedResult<T>> GetPagedAsync(
            int pageNumber,
            int pageSize,
            params Expression<Func<T, object>>[] includes
        )
        {
            if (pageNumber < 1)
                pageNumber = 1;
            if (pageSize < 1)
                pageSize = 20;
            if (pageSize > 100)
                pageSize = 100;

            IQueryable<T> query = _dbSet.AsNoTracking();
            var totalCount = await query.CountAsync();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var items = await query
                .OrderBy(e => EF.Property<int>(e, "Id"))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<T>(items, totalCount, pageNumber, pageSize);
        }

        public virtual async Task<T?> GetByIdAsync(
            int id,
            params Expression<Func<T, object>>[] includes
        )
        {
            if (includes == null || includes.Length == 0)
            {
                return await _dbSet.FindAsync(id);
            }

            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void HardDelete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual async Task SoftDeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                entity.DeletedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
