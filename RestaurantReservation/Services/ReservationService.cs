using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class ReservationService(IReservationRepository repository)
{
    public Reservation CreateReservation(int customerId, int restaurantId, int tableId, DateTime reservationDate,
        int partySize)
    {
        var reservation = new Reservation
        {
            CustomerId = customerId,
            RestaurantId = restaurantId,
            TableId = tableId,
            ReservationDate = reservationDate,
            PartySize = partySize
        };
        return repository.Create(reservation);
    }

    public Reservation? UpdateReservation(int id, int? customerId = null, int? restaurantId = null, int? tableId = null,
        DateTime? reservationDate = null, int? partySize = null)
    {
        var reservation = repository.GetById(id);
        if (reservation == null) return null;

        if (customerId.HasValue) reservation.CustomerId = customerId.Value;
        if (restaurantId.HasValue) reservation.RestaurantId = restaurantId.Value;
        if (tableId.HasValue) reservation.TableId = tableId.Value;
        if (reservationDate.HasValue) reservation.ReservationDate = reservationDate.Value;
        if (partySize.HasValue) reservation.PartySize = partySize.Value;

        return repository.Update(reservation);
    }

    public bool DeleteReservation(int id)
    {
        return repository.Delete(id);
    }
}