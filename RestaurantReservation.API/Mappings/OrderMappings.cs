using RestaurantReservation.API.Dtos;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.Mappings;

public static class OrderMappings
{
    public static OrderResponseDto ToDto(this Order order) =>
        new(
            order.OrderId,
            order.ReservationId,
            order.EmployeeId,
            order.OrderDate,
            order.TotalAmount,
            order.OrderItems.Select(oi => oi.ToDto()).ToList()
        );

    public static OrderItemResponseDto ToDto(this OrderItem orderItem) =>
        new(
            orderItem.OrderItemId,
            orderItem.OrderId,
            orderItem.ItemId,
            orderItem.Quantity,
            orderItem.Item.ToDto()
        );
}