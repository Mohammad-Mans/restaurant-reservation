namespace RestaurantReservation.API.Dtos;

public record ReservationResponseDto(
    int ReservationId,
    int CustomerId,
    int RestaurantId,
    int TableId,
    DateTime ReservationDate,
    int PartySize
);