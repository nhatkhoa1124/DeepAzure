using DeepAzureServer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeepAzureServer.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ConfigureBaseAuditable();

            builder.ToTable("Users");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(u => u.PasswordHash)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(u => u.PasswordSalt)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(u => u.DisplayName)
                .HasMaxLength(100)
                .IsRequired(false);
            builder.Property(u => u.Email)
                .HasMaxLength(100)
                .IsRequired();
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
                .HasDefaultValue(0)
                .IsRequired();

            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
