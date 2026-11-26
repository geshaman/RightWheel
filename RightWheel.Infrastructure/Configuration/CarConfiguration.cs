using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RightWheel.Domain.Models;

namespace RightWheel.Infrastructure.Configuration
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("cars");

            builder.HasKey(c => c.Id);

            builder.HasIndex(x => new { x.Brand, x.Model, x.Engine })
                .IsUnique();

            builder.Property(c => c.Brand)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Model)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Engine)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(
                new Car { Id = 1, Brand = "Toyota", Model = "Carina", Engine = "1.5 л" },
                new Car { Id = 2, Brand = "Toyota", Model = "Corona", Engine = "1.8 л" },
                new Car { Id = 3, Brand = "Toyota", Model = "Almera", Engine = "1.5 л" });
        }
    }
}
