using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasOne(o => o.Reservation)
            .WithMany(r => r.Orders)
            .HasForeignKey(o => o.ReservationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.Employee)
            .WithMany(e => e.Orders)
            .HasForeignKey(o => o.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Order { OrderId = 1, ReservationId = 1, EmployeeId = 2, OrderDate = new DateTime(2025, 11, 15, 19, 15, 0), TotalAmount = 35.49m },
            new Order { OrderId = 2, ReservationId = 2, EmployeeId = 2, OrderDate = new DateTime(2025, 11, 16, 20, 10, 0), TotalAmount = 82.50m },
            new Order { OrderId = 3, ReservationId = 3, EmployeeId = 3, OrderDate = new DateTime(2025, 11, 17, 18, 45, 0), TotalAmount = 149.94m },
            new Order { OrderId = 4, ReservationId = 4, EmployeeId = 3, OrderDate = new DateTime(2025, 11, 18, 19, 45, 0), TotalAmount = 29.99m },
            new Order { OrderId = 5, ReservationId = 5, EmployeeId = 5, OrderDate = new DateTime(2025, 11, 19, 20, 15, 0), TotalAmount = 287.92m }
        );
    }
}