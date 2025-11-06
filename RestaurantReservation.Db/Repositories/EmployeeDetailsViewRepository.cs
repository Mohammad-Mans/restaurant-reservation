using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class EmployeeDetailsViewRepository(RestaurantReservationDbContext context) : IEmployeeDetailsViewRepository
{
    public async Task<List<EmployeeDetailsView>> GetAllAsync()
    {
        return await context.EmployeeDetailsView.ToListAsync();
    }

    public async Task<EmployeeDetailsView?> GetByEmployeeIdAsync(int employeeId)
    {
        return await context.EmployeeDetailsView
            .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
    }

    public async Task<List<EmployeeDetailsView>> GetByRestaurantIdAsync(int restaurantId)
    {
        return await context.EmployeeDetailsView
            .Where(e => e.RestaurantId == restaurantId)
            .ToListAsync();
    }
}