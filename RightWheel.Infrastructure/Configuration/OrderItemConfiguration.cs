using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RightWheel.Domain.Models;

namespace RightWheel.Infrastructure.Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("order_items");

            builder.HasKey(x => new {x.OrderId, x.PartId});

            builder.Property(oi => oi.PartQuantity);

            builder.HasOne<Part>()
                .WithMany()
                .HasForeignKey(oi => oi.PartId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
