using DeepAzureServer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeepAzureServer.Data.Configurations
{
    public class ElementConfiguration : IEntityTypeConfiguration<Element>
    {
        public void Configure(EntityTypeBuilder<Element> builder)
        {
            builder.ConfigureBaseAuditable();

            builder.ToTable("Elements");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(e => e.Description)
                .HasMaxLength(250)
                .IsRequired();
        }
    }
}
