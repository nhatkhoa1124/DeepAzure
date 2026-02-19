using System.Linq.Expressions;
using DeepAzureServer.Data;
using DeepAzureServer.Models.Common;
using DeepAzureServer.Models.Entities;
using DeepAzureServer.Repositories.Interfaces;

namespace DeepAzureServer.Repositories.Implementations
{
    public class MonsterRepository : Repository<Monster>, IMonsterRepository
    {
        public MonsterRepository(AppDbContext context)
            : base(context) { }

        public override async Task<PagedResult<Monster>> GetPagedAsync(
            int pageNumber,
            int pageSize,
            params Expression<Func<Monster, object>>[] includes
        )
        {
            return await base.GetPagedAsync(
                pageNumber,
                pageSize,
                m => m.PrimaryElement,
                m => m.SecondaryElement,
                m => m.Ability
            );
        }

        public override async Task<Monster?> GetByIdAsync(
            int id,
            params Expression<Func<Monster, object>>[] includes
        )
        {
            return await base.GetByIdAsync(
                id,
                m => m.PrimaryElement,
                m => m.SecondaryElement,
                m => m.Ability
            );
        }
    }
}
