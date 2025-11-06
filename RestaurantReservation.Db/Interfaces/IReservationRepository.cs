using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Interfaces;

public interface IReservationRepository
{
    Task<Reservation> CreateAsync(Reservation reservation);
    Task<Reservation?> GetByIdAsync(int id);
    Task<List<Reservation>> GetAllAsync();
    Task<Reservation?> UpdateAsync(Reservation reservation);
    Task<bool> DeleteAsync(int id);
    Task<List<Reservation>> GetByCustomerIdAsync(int customerId);
}