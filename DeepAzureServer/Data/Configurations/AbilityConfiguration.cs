using DeepAzureServer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeepAzureServer.Data.Configurations
{
    public class AbilityConfiguration : IEntityTypeConfiguration<Ability>
    {
        public void Configure(EntityTypeBuilder<Ability> builder)
        {
            builder.ConfigureBaseAuditable();

            builder.ToTable("Abilities");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                .HasMaxLength(25)
                .IsRequired();
            builder.Property(a => a.Description)
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(a => a.LogicKey)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(a => a.LogicData)
                .IsRequired();
        }
    }
}
