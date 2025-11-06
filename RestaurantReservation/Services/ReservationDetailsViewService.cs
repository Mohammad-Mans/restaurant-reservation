using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class ReservationDetailsViewService(IReservationDetailsViewRepository repository)
{
    public async Task<List<ReservationDetailsView>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<ReservationDetailsView?> GetByReservationIdAsync(int reservationId)
    {
        return await repository.GetByReservationIdAsync(reservationId);
    }

    public async Task<List<ReservationDetailsView>> GetByCustomerIdAsync(int customerId)
    {
        return await repository.GetByCustomerIdAsync(customerId);
    }

    public async Task<List<ReservationDetailsView>> GetByRestaurantIdAsync(int restaurantId)
    {
        return await repository.GetByRestaurantIdAsync(restaurantId);
    }
}