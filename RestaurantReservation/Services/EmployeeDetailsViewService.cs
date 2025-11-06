using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class EmployeeDetailsViewService(IEmployeeDetailsViewRepository repository)
{
    public async Task<List<EmployeeDetailsView>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<EmployeeDetailsView?> GetByEmployeeIdAsync(int employeeId)
    {
        return await repository.GetByEmployeeIdAsync(employeeId);
    }

    public async Task<List<EmployeeDetailsView>> GetByRestaurantIdAsync(int restaurantId)
    {
        return await repository.GetByRestaurantIdAsync(restaurantId);
    }
}