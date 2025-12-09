using RestaurantReservation.API.Dtos;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.Mappings;

public static class ReservationMappings
{
    public static ReservationResponseDto ToDto(this Reservation reservation) =>
        new(
            reservation.ReservationId,
            reservation.CustomerId,
            reservation.RestaurantId,
            reservation.TableId,
            reservation.ReservationDate,
            reservation.PartySize
        );
}