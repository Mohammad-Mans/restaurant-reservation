using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class ReservationDetailsViewRepository(RestaurantReservationDbContext context)
    : IReservationDetailsViewRepository
{
    public List<ReservationDetailsView> GetAll()
    {
        return context.ReservationDetailsView.ToList();
    }

    public ReservationDetailsView? GetByReservationId(int reservationId)
    {
        return context.ReservationDetailsView
            .FirstOrDefault(r => r.ReservationId == reservationId);
    }

    public List<ReservationDetailsView> GetByCustomerId(int customerId)
    {
        return context.ReservationDetailsView
            .Where(r => r.CustomerId == customerId)
            .ToList();
    }

    public List<ReservationDetailsView> GetByRestaurantId(int restaurantId)
    {
        return context.ReservationDetailsView
            .Where(r => r.RestaurantId == restaurantId)
            .ToList();
    }
}