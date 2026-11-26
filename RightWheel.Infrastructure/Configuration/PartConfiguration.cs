using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RightWheel.Domain.Models;
namespace RightWheel.Infrastructure.Configuration
{
    public class PartConfiguration : IEntityTypeConfiguration<Part>
    {
        public void Configure(EntityTypeBuilder<Part> builder)
        {
            builder.ToTable("parts", t =>
            {
                t.HasCheckConstraint("CK_Parts_Quantity_NonNegative", "\"Quantity\" >= 0");
            });
            
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.Property(p => p.Price)
                .HasPrecision(12, 2);

            builder.Property(p => p.Position)
                .HasConversion<string>()
                .HasMaxLength(10);

            builder.Property(p => p.Quantity);

            builder.HasMany(p => p.Cars)
                .WithMany(c => c.Parts);

            builder.HasMany(p => p.Images)
                .WithOne()
                .HasForeignKey(pi => pi.PartId);

            builder.HasMany(p => p.Categories)
                .WithMany();
        }
    }
}
