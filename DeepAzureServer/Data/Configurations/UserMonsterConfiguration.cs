using DeepAzureServer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeepAzureServer.Data.Configurations
{
    public class UserMonsterConfiguration : IEntityTypeConfiguration<UserMonster>
    {
        public void Configure(EntityTypeBuilder<UserMonster> builder)
        {
            builder.ConfigureBaseAuditable();

            builder.ToTable("UserMonsters");
            builder.HasKey(um => um.Id);

            builder.Property(um => um.Nickname)
                .HasMaxLength(50)
                .IsRequired(false);
            builder.Property(um => um.Level)
                .HasDefaultValue(1)
                .IsRequired();
            builder.Property(um => um.Exp)
                .HasDefaultValue(0)
                .IsRequired();
            builder.Property(um => um.TeamSlot)
                .HasDefaultValue(0) // Not on current team
                .IsRequired();

            builder.HasOne(um => um.Monster)
                .WithMany(m => m.UserMonsters)
                .HasForeignKey(um => um.MonsterId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(um => um.Owner)
                .WithMany(o => o.UserMonsters)
                .HasForeignKey(um => um.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(um => um.Move1)
                .WithMany()
                .HasForeignKey(um => um.Move1Id)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(um => um.Move2)
                .WithMany()
                .HasForeignKey(um => um.Move2Id)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(um => um.Move3)
                .WithMany()
                .HasForeignKey(um => um.Move3Id)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(um => um.Move4)
                .WithMany()
                .HasForeignKey(um => um.Move4Id)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
