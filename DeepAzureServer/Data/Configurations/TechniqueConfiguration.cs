using DeepAzureServer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeepAzureServer.Data.Configurations
{
    public class TechniqueConfiguration : IEntityTypeConfiguration<Technique>
    {
        public void Configure(EntityTypeBuilder<Technique> builder)
        {
            builder.ConfigureBaseAuditable();

            builder.ToTable("Techniques");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(t => t.Description)
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(t => t.StrengthDamage)
                .IsRequired();
            builder.Property(t => t.MagicDamage)
                .IsRequired();
            builder.Property(t => t.Target)
                .IsRequired();
            builder.Property(t => t.StatType)
                .IsRequired();
            builder.Property(t => t.StatAmout)
                .IsRequired();

            builder.HasOne(t => t.Element)
                .WithMany(e => e.Techniques)
                .HasForeignKey(t => t.ElementId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.StatusEffect)
                .WithMany(se => se.Techniques)
                .HasForeignKey(t => t.StatusEffectId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
