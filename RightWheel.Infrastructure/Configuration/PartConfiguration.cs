
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
                t.HasCheckConstraint(
                    "CK_Parts_Quantity_NonNegative",
                    "\"Quantity\" >= 0"
                );
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

            builder.HasData(
                new Part
                {
                    Id = 1,
                    Name = "Тормозные колодки Bosch",
                    Price = 3500m,
                    Quantity = 25,
                    Description = "Передние тормозные колодки Bosch. Высокая износостойкость и эффективное торможение.",
                    Position = null
                },
                new Part
                {
                    Id = 2,
                    Name = "Масляный фильтр Mann-Filter",
                    Price = 1200m,
                    Quantity = 40,
                    Description = "Масляный фильтр для очистки моторного масла от загрязнений.",
                    Position = null
                },
                new Part
                {
                    Id = 3,
                    Name = "Амортизатор KYB Excel-G",
                    Price = 7800m,
                    Quantity = 12,
                    Description = "Газомасляный амортизатор для обеспечения устойчивости и комфорта автомобиля.",
                    Position = null
                }
            );
        }
    }
}
