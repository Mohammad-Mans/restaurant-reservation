using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class EmployeeDetailsViewRepository(RestaurantReservationDbContext context) : IEmployeeDetailsViewRepository
{
    public List<EmployeeDetailsView> GetAll()
    {
        return context.EmployeeDetailsView.ToList();
    }

    public EmployeeDetailsView? GetByEmployeeId(int employeeId)
    {
        return context.EmployeeDetailsView
            .FirstOrDefault(e => e.EmployeeId == employeeId);
    }

    public List<EmployeeDetailsView> GetByRestaurantId(int restaurantId)
    {
        return context.EmployeeDetailsView
            .Where(e => e.RestaurantId == restaurantId)
            .ToList();
    }
}