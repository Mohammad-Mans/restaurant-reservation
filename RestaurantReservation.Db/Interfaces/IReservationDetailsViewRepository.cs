using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Interfaces;

public interface IReservationDetailsViewRepository
{
    Task<List<ReservationDetailsView>> GetAllAsync();
    Task<ReservationDetailsView?> GetByReservationIdAsync(int reservationId);
    Task<List<ReservationDetailsView>> GetByCustomerIdAsync(int customerId);
    Task<List<ReservationDetailsView>> GetByRestaurantIdAsync(int restaurantId);
}