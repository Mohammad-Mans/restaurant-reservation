namespace RestaurantReservation.API.Dtos;

public record CreateReservationDto(
    int CustomerId,
    int RestaurantId,
    int TableId,
    DateTime ReservationDate,
    int PartySize
);