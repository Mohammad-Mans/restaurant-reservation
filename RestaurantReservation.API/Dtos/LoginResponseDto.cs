namespace RestaurantReservation.API.Dtos;

public record LoginResponseDto(
    string Token,
    string Username,
    string UserType,
    int UserId
);