using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Interfaces;

public interface IEmployeeDetailsViewRepository
{
    Task<List<EmployeeDetailsView>> GetAllAsync();
    Task<EmployeeDetailsView?> GetByEmployeeIdAsync(int employeeId);
    Task<List<EmployeeDetailsView>> GetByRestaurantIdAsync(int restaurantId);
}