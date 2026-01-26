using DeepAzureServer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeepAzureServer.Data.Configurations
{
    public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.ConfigureBaseAuditable();

            builder.ToTable("Conversations");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.LastMessageAt)
                .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'")
                .IsRequired();

            builder.HasOne(c => c.User1)
                .WithMany()
                .HasForeignKey(c => c.User1Id)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(c => c.User2)
                .WithMany()
                .HasForeignKey(c => c.User2Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
