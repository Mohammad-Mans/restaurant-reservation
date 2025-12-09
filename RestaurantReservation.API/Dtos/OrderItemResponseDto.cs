namespace RestaurantReservation.API.Dtos;

public record OrderItemResponseDto(
    int OrderItemId,
    int OrderId,
    int ItemId,
    int Quantity,
    MenuItemResponseDto Item
);