using DeepAzureServer.Data;
using DeepAzureServer.Models.Entities;
using DeepAzureServer.Repositories.Interfaces;

namespace DeepAzureServer.Repositories.Implementations
{
    public class AbilityRepository : Repository<Ability>, IAbilityRepository
    {
        public AbilityRepository(AppDbContext context)
            : base(context) { }
    }
}
