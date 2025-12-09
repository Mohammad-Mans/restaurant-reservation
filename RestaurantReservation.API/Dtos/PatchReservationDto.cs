namespace RestaurantReservation.API.Dtos;

public record PatchReservationDto(
    int? CustomerId = null,
    int? RestaurantId = null,
    int? TableId = null,
    DateTime? ReservationDate = null,
    int? PartySize = null
);