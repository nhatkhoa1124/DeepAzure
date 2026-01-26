using DeepAzureServer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeepAzureServer.Data.Configurations
{
    public class ElementMatchupConfiguration : IEntityTypeConfiguration<ElementMatchup>
    {
        public void Configure(EntityTypeBuilder<ElementMatchup> builder)
        {
            builder.ConfigureBaseAuditable();

            builder.ToTable("ElementMatchups");
            builder.HasKey(em => em.Id);

            builder.Property(em => em.Multiplier)
                .HasDefaultValue(1)
                .IsRequired();

            builder.HasOne(em => em.Attacker)
                .WithMany()
                .HasForeignKey(em => em.AttackerId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(em => em.Defender)
                .WithMany()
                .HasForeignKey(em => em.DefenderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
