using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Interfaces;

public interface IOrderItemRepository
{
    Task<OrderItem> CreateAsync(OrderItem orderItem);
    Task<OrderItem?> GetByIdAsync(int id);
    Task<List<OrderItem>> GetAllAsync();
    Task<OrderItem?> UpdateAsync(OrderItem orderItem);
    Task<bool> DeleteAsync(int id);
}