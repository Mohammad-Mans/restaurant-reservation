using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Interfaces;

public interface IMenuItemRepository
{
    Task<MenuItem> CreateAsync(MenuItem menuItem);
    Task<MenuItem?> GetByIdAsync(int id);
    Task<List<MenuItem>> GetAllAsync();
    Task<MenuItem?> UpdateAsync(MenuItem menuItem);
    Task<bool> DeleteAsync(int id);
    Task<List<MenuItem>> GetByReservationIdAsync(int reservationId);
}