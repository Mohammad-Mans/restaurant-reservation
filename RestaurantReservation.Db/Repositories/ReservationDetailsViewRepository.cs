using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class ReservationDetailsViewRepository(RestaurantReservationDbContext context)
    : IReservationDetailsViewRepository
{
    public async Task<List<ReservationDetailsView>> GetAllAsync()
    {
        return await context.ReservationDetailsView.ToListAsync();
    }

    public async Task<ReservationDetailsView?> GetByReservationIdAsync(int reservationId)
    {
        return await context.ReservationDetailsView
            .FirstOrDefaultAsync(r => r.ReservationId == reservationId);
    }

    public async Task<List<ReservationDetailsView>> GetByCustomerIdAsync(int customerId)
    {
        return await context.ReservationDetailsView
            .Where(r => r.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task<List<ReservationDetailsView>> GetByRestaurantIdAsync(int restaurantId)
    {
        return await context.ReservationDetailsView
            .Where(r => r.RestaurantId == restaurantId)
            .ToListAsync();
    }
}