namespace RestaurantReservation.API.Dtos;

public record AverageOrderAmountResponseDto(
    int EmployeeId,
    decimal? AverageAmount
);