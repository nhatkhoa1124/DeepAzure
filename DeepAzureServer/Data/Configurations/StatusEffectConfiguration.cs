
using DeepAzureServer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeepAzureServer.Data.Configurations
{
    public class StatusEffectConfiguration : IEntityTypeConfiguration<StatusEffect>
    {
        public void Configure(EntityTypeBuilder<StatusEffect> builder)
        {
            builder.ConfigureBaseAuditable();

            builder.ToTable("StatusEffects");
            builder.HasKey(se => se.Id);

            builder.Property(se => se.Name)
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(se => se.Description)
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(se => se.EffectType)
                .IsRequired();
        }
    }
}
