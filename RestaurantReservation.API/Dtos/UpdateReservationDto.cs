namespace RestaurantReservation.API.Dtos;

public record UpdateReservationDto(
    int CustomerId,
    int RestaurantId,
    int TableId,
    DateTime ReservationDate,
    int PartySize
);