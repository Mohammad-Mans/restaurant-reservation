using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class ReservationService(IReservationRepository repository)
{
    public async Task<Reservation> CreateReservationAsync(int customerId, int restaurantId, int tableId, DateTime reservationDate,
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
        return await repository.CreateAsync(reservation);
    }

    public async Task<Reservation?> UpdateReservationAsync(int id, int? customerId = null, int? restaurantId = null, int? tableId = null,
        DateTime? reservationDate = null, int? partySize = null)
    {
        var reservation = await repository.GetByIdAsync(id);
        if (reservation == null) return null;

        if (customerId.HasValue) reservation.CustomerId = customerId.Value;
        if (restaurantId.HasValue) reservation.RestaurantId = restaurantId.Value;
        if (tableId.HasValue) reservation.TableId = tableId.Value;
        if (reservationDate.HasValue) reservation.ReservationDate = reservationDate.Value;
        if (partySize.HasValue) reservation.PartySize = partySize.Value;

        return await repository.UpdateAsync(reservation);
    }

    public async Task<bool> DeleteReservationAsync(int id)
    {
        return await repository.DeleteAsync(id);
    }

    public async Task<List<Reservation>> GetReservationsByCustomerAsync(int customerId)
    {
        return await repository.GetByCustomerIdAsync(customerId);
    }
}