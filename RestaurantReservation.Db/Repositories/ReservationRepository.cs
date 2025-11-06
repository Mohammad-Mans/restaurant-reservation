using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class ReservationRepository(RestaurantReservationDbContext context) : IReservationRepository
{
    public async Task<Reservation> CreateAsync(Reservation reservation)
    {
        context.Reservations.Add(reservation);
        await context.SaveChangesAsync();
        return reservation;
    }

    public async Task<Reservation?> GetByIdAsync(int id)
    {
        return await context.Reservations.FindAsync(id);
    }

    public async Task<List<Reservation>> GetAllAsync()
    {
        return await context.Reservations.ToListAsync();
    }

    public async Task<Reservation?> UpdateAsync(Reservation reservation)
    {
        var existing = await context.Reservations.FindAsync(reservation.ReservationId);
        if (existing == null) return null;

        existing.CustomerId = reservation.CustomerId;
        existing.RestaurantId = reservation.RestaurantId;
        existing.TableId = reservation.TableId;
        existing.ReservationDate = reservation.ReservationDate;
        existing.PartySize = reservation.PartySize;

        await context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var reservation = await context.Reservations.FindAsync(id);
        if (reservation == null) return false;

        context.Reservations.Remove(reservation);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Reservation>> GetByCustomerIdAsync(int customerId)
    {
        return await context.Reservations.Where(r => r.CustomerId == customerId).ToListAsync();
    }
}