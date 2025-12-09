namespace RestaurantReservation.API.Dtos;

public record EmployeeResponseDto(
    int EmployeeId,
    int RestaurantId,
    string FirstName,
    string LastName,
    string Position
);