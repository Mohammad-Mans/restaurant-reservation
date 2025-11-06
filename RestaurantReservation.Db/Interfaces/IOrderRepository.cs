using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Interfaces;

public interface IOrderRepository
{
    Task<Order> CreateAsync(Order order);
    Task<Order?> GetByIdAsync(int id);
    Task<List<Order>> GetAllAsync();
    Task<Order?> UpdateAsync(Order order);
    Task<bool> DeleteAsync(int id);
    Task<List<Order>> GetByReservationIdAsync(int reservationId);
    Task<decimal?> GetAverageOrderAmountByEmployeeIdAsync(int employeeId);
}