using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class ReservationRepository(RestaurantReservationDbContext context) : IReservationRepository
{
    public Reservation Create(Reservation reservation)
    {
        context.Reservations.Add(reservation);
        context.SaveChanges();
        return reservation;
    }

    public Reservation? GetById(int id)
    {
        return context.Reservations.Find(id);
    }

    public List<Reservation> GetAll()
    {
        return context.Reservations.ToList();
    }

    public Reservation? Update(Reservation reservation)
    {
        var existing = context.Reservations.Find(reservation.ReservationId);
        if (existing == null) return null;

        existing.CustomerId = reservation.CustomerId;
        existing.RestaurantId = reservation.RestaurantId;
        existing.TableId = reservation.TableId;
        existing.ReservationDate = reservation.ReservationDate;
        existing.PartySize = reservation.PartySize;

        context.SaveChanges();
        return existing;
    }

    public bool Delete(int id)
    {
        var reservation = context.Reservations.Find(id);
        if (reservation == null) return false;

        context.Reservations.Remove(reservation);
        context.SaveChanges();
        return true;
    }
}