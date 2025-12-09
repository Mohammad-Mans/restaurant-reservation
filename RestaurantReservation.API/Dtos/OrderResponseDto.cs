namespace RestaurantReservation.API.Dtos;

public record OrderResponseDto(
    int OrderId,
    int ReservationId,
    int EmployeeId,
    DateTime OrderDate,
    decimal TotalAmount,
    List<OrderItemResponseDto> OrderItems
);