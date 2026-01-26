using DeepAzureServer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeepAzureServer.Data.Configurations
{
    public class MonsterConfiguration : IEntityTypeConfiguration<Monster>
    {
        public void Configure(EntityTypeBuilder<Monster> builder)
        {
            builder.ConfigureBaseAuditable();

            builder.ToTable("Monsters");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name)
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(m => m.Description)
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(m => m.BaseHealth)
                .IsRequired();
            builder.Property(m => m.GrowthHealth)
                .IsRequired();
            builder.Property(m => m.BaseStrength)
                .IsRequired();
            builder.Property(m => m.GrowthStrength)
                .IsRequired();
            builder.Property(m => m.BaseDefense)
                .IsRequired();
            builder.Property(m => m.GrowthDefense)
                .IsRequired();
            builder.Property(m => m.BaseMagic)
                .IsRequired();
            builder.Property(m => m.GrowthMagic)
                .IsRequired();
            builder.Property(m => m.BaseResistance)
                .IsRequired();
            builder.Property(m => m.GrowthResistance)
                .IsRequired();
            builder.Property(m => m.BaseSpeed)
                .IsRequired();
            builder.Property(m => m.GrowthSpeed)
                .IsRequired();
            builder.Property(m => m.Price)
                .IsRequired(false);

            builder.HasOne(m => m.PrimaryElement)
                .WithMany()
                .HasForeignKey(m => m.PrimaryElementId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.SecondaryElement)
                .WithMany()
                .HasForeignKey(m => m.SecondaryElementId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(m => m.Ability)
                .WithMany(a => a.Monsters)
                .HasForeignKey(m => m.AbilityId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
