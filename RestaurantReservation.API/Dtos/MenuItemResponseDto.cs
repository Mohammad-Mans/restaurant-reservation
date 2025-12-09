namespace RestaurantReservation.API.Dtos;

public record MenuItemResponseDto(
    int ItemId,
    int RestaurantId,
    string Name,
    string? Description,
    decimal Price
);