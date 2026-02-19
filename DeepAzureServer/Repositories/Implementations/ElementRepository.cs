using DeepAzureServer.Data;
using DeepAzureServer.Models.Entities;
using DeepAzureServer.Repositories.Interfaces;

namespace DeepAzureServer.Repositories.Implementations
{
    public class ElementRepository : Repository<Element>, IElementRepository
    {
        public ElementRepository(AppDbContext context)
            : base(context) { }
    }
}
