using DeepAzureServer.Data;
using DeepAzureServer.Models.Common;
using DeepAzureServer.Models.Entities;
using DeepAzureServer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeepAzureServer.Repositories.Implementations
{
    public class MonsterRepository : IMonsterRepository
    {
        private readonly AppDbContext _context;

        public MonsterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var monster = await _context.Monsters.FindAsync(id);
            if (monster == null || monster.DeletedAt != null)
                return;
            monster.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<Monster>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            if (pageNumber < 1)
                pageNumber = 1;
            if (pageSize < 1)
                pageSize = 20;
            if (pageSize > 100)
                pageSize = 100;

            var totalCount = await _context.Monsters.CountAsync();

            var monsters = await _context
                .Monsters.AsNoTracking()
                .Include(m => m.PrimaryElement)
                .Include(m => m.SecondaryElement)
                .Include(m => m.Ability)
                .OrderBy(m => m.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Monster>(monsters, totalCount, pageNumber, pageSize);
        }

        public async Task<Monster?> GetByIdAsync(int id)
        {
            return await _context
                .Monsters.Include(m => m.PrimaryElement)
                .Include(m => m.SecondaryElement)
                .Include(m => m.Ability)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
