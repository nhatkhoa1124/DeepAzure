using DeepAzureServer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeepAzureServer.Data.Configurations
{
    public static class BaseAuditableConfiguration
    {
        public static void ConfigureBaseAuditable<T>(this EntityTypeBuilder<T> builder) where T : BaseAuditable
        {
            builder.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");
            builder.Property(e => e.UpdatedAt)
                .IsRequired(false);
            builder.Property(e => e.DeletedAt)
                .IsRequired(false);
        }
    }
}
