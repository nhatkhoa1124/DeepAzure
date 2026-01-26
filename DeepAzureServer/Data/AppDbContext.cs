using Microsoft.EntityFrameworkCore;

namespace DeepAzureServer.Data
{
    public class AppDbContext : DbContext
    {
        private readonly DbContext _context;


        public AppDbContext(DbContext context)
        {
            _context = context;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
