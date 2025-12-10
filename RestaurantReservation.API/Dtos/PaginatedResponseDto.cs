namespace RestaurantReservation.API.Dtos;

public record PaginatedResponseDto<T>(
    List<T> Data,
    int PageNumber,
    int PageSize,
    int TotalCount,
    int TotalPages
);