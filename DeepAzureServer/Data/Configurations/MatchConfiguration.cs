using DeepAzureServer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeepAzureServer.Data.Configurations
{
    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.ConfigureBaseAuditable();

            builder.ToTable("Matches");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Status)
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(m => m.MatchType)
                .HasMaxLength(15)
                .IsRequired();
            builder.Property(m => m.EndedAt)
                .IsRequired(false);
            builder.Property(m => m.ReplayLog)
                .IsRequired(false);
        }
    }
}
