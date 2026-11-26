using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RightWheel.Domain.Models;

namespace RightWheel.Infrastructure.Configuration
{
    public class PartImageConfiguration : IEntityTypeConfiguration<PartImage>
    {
        public void Configure(EntityTypeBuilder<PartImage> builder)
        {
            builder.ToTable("part_images");

            builder.HasKey(pi => pi.Id);

            builder.Property(pi => pi.ImageUrl)
                .IsRequired()
                .HasMaxLength(300);
        }
    }
}
