namespace RestaurantReservation.API.Dtos;

public record CustomerResponseDto(
    int CustomerId,
    string FirstName,
    string LastName,
    string? Email,
    string? PhoneNumber
);