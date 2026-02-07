using DeepAzureServer.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeepAzureServer.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<long>, long>
    {

        public DbSet<Ability> Abilities { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Element> Elements { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Monster> Monsters { get; set; }
        public DbSet<StatusEffect> StatusEffects { get; set; }
        public DbSet<Technique> Techniques { get; set; }
        public DbSet<UserBadge> UserBadges { get; set; }
        public DbSet<UserItem> UserItems { get; set; }
        public DbSet<UserMatch> UserMatches { get; set; }
        public DbSet<UserMonster> UserMonsters { get; set; }
        public DbSet<ElementMatchup> ElementMatchups { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Entity<IdentityRole<long>>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<long>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<long>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityRoleClaim<long>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserLogin<long>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserToken<long>>().ToTable("UserTokens");

            modelBuilder.Entity<User>().HasQueryFilter(u => u.DeletedAt == null);
            modelBuilder.Entity<Ability>().HasQueryFilter(a => a.DeletedAt == null);
            modelBuilder.Entity<Badge>().HasQueryFilter(b => b.DeletedAt == null);
            modelBuilder.Entity<Conversation>().HasQueryFilter(c => c.DeletedAt == null);
            modelBuilder.Entity<Element>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<ElementMatchup>().HasQueryFilter(em => em.DeletedAt == null);
            modelBuilder.Entity<Item>().HasQueryFilter(i => i.DeletedAt == null);
            modelBuilder.Entity<Match>().HasQueryFilter(m => m.DeletedAt == null);
            modelBuilder.Entity<Message>().HasQueryFilter(m => m.DeletedAt == null);
            modelBuilder.Entity<Monster>().HasQueryFilter(m => m.DeletedAt == null);
            modelBuilder.Entity<StatusEffect>().HasQueryFilter(se => se.DeletedAt == null);
            modelBuilder.Entity<Technique>().HasQueryFilter(t => t.DeletedAt == null);
            modelBuilder.Entity<UserBadge>().HasQueryFilter(ub => ub.DeletedAt == null);
            modelBuilder.Entity<UserItem>().HasQueryFilter(ui => ui.DeletedAt == null);
            modelBuilder.Entity<UserMatch>().HasQueryFilter(um => um.DeletedAt == null);
            modelBuilder.Entity<UserMonster>().HasQueryFilter(um => um.DeletedAt == null);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseAuditable>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    if(entry.Entity.CreatedAt == default)
                    { 
                        entry.Entity.CreatedAt = DateTime.UtcNow; 
                    }
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
