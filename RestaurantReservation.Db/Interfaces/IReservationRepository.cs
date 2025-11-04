using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Interfaces;

public interface IReservationRepository
{
    Reservation Create(Reservation reservation);
    Reservation? GetById(int id);
    List<Reservation> GetAll();
    Reservation? Update(Reservation reservation);
    bool Delete(int id);
}