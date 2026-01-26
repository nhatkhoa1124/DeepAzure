using DeepAzureServer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeepAzureServer.Data.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ConfigureBaseAuditable();

            builder.ToTable("Items");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(i => i.Description)
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(i => i.EffectType)
                .IsRequired();
            builder.Property(i => i.EffectPower)
                .HasDefaultValue(1)
                .IsRequired();
            builder.Property(i => i.Price)
                .IsRequired(false);
        }
    }
}
