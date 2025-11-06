using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(oi => oi.Item)
            .WithMany(mi => mi.OrderItems)
            .HasForeignKey(oi => oi.ItemId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new OrderItem { OrderItemId = 1, OrderId = 1, ItemId = 1, Quantity = 1 },
            new OrderItem { OrderItemId = 2, OrderId = 1, ItemId = 2, Quantity = 1 },
            new OrderItem { OrderItemId = 3, OrderId = 2, ItemId = 2, Quantity = 5 },
            new OrderItem { OrderItemId = 4, OrderId = 3, ItemId = 3, Quantity = 6 },
            new OrderItem { OrderItemId = 5, OrderId = 4, ItemId = 4, Quantity = 1 }
        );
    }
}