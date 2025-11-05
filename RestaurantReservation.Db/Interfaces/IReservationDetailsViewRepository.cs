using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Interfaces;

public interface IReservationDetailsViewRepository
{
    List<ReservationDetailsView> GetAll();
    ReservationDetailsView? GetByReservationId(int reservationId);
    List<ReservationDetailsView> GetByCustomerId(int customerId);
    List<ReservationDetailsView> GetByRestaurantId(int restaurantId);
}