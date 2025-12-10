namespace RestaurantReservation.API.Dtos;

public record PaginationQueryDto(
    int? PageNumber,
    int? PageSize
);