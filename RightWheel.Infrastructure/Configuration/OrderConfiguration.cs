using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RightWheel.Domain.Models;
using System.Security.Cryptography.X509Certificates;

namespace RightWheel.Infrastructure.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.ClientName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(o => o.ClientPhoneNumber)
               .IsRequired()
               .HasMaxLength(20);

            builder.Property(o => o.VIN)
                .HasMaxLength(17);

            builder.Property(o => o.Status)
               .HasConversion<string>()
               .HasMaxLength(9)
               .IsRequired();

            builder.Property(o => o.OrderCreateTime);

            builder.Property(o => o.OrderEndTime);

            builder.HasMany(o => o.Items)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId);

        }
    }
}
