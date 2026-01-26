using DeepAzureServer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeepAzureServer.Data.Configurations
{
    public class UserMatchConfiguration : IEntityTypeConfiguration<UserMatch>
    {
        public void Configure(EntityTypeBuilder<UserMatch> builder)
        {
            builder.ConfigureBaseAuditable();

            builder.ToTable("UserMatches");
            builder.HasKey(um => new { um.UserId, um.MatchId });

            builder.Property(um => um.TeamSnapshot)
                .IsRequired();
            builder.Property(um => um.EloChange)
                .HasDefaultValue(0)
                .IsRequired();
            builder.Property(um => um.Outcome)
                .IsRequired();
            builder.Property(um => um.GoldEarned)
                .HasDefaultValue(0)
                .IsRequired();

            builder.HasOne(um => um.User)
                .WithMany(u => u.UserMatches)
                .HasForeignKey(um => um.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(um => um.Match)
                .WithMany(m => m.UserMatches)
                .HasForeignKey(um => um.MatchId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
