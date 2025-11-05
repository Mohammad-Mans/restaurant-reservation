using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class ReservationDetailsViewService(IReservationDetailsViewRepository repository)
{
    public List<ReservationDetailsView> GetAll()
    {
        return repository.GetAll();
    }

    public ReservationDetailsView? GetByReservationId(int reservationId)
    {
        return repository.GetByReservationId(reservationId);
    }

    public List<ReservationDetailsView> GetByCustomerId(int customerId)
    {
        return repository.GetByCustomerId(customerId);
    }

    public List<ReservationDetailsView> GetByRestaurantId(int restaurantId)
    {
        return repository.GetByRestaurantId(restaurantId);
    }
}