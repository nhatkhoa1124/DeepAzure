using DeepAzureServer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeepAzureServer.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.ToTable("Users");

            builder.Property(u => u.DisplayName)
                .HasMaxLength(100)
                .IsRequired(false);
            builder.Property(u => u.LastActive)
                .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'")
                .IsRequired();
            builder.Property(u => u.AvatarUrl)
                .HasMaxLength(500)
                .IsRequired(false);
            builder.Property(u => u.AvatarPublicId)
                .HasMaxLength(255)
                .IsRequired(false);
            builder.Property(u => u.EloRating)
                .HasDefaultValue(-1)
                .IsRequired();
            builder.Property(u => u.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");
            builder.Property(u => u.UpdatedAt)
                .IsRequired(false);
            builder.Property(u => u.DeletedAt)
                .IsRequired(false);
        }
    }
}
