using DeepAzureServer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeepAzureServer.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ConfigureBaseAuditable();

            builder.ToTable("Roles");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
