using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class EmployeeDetailsViewService(IEmployeeDetailsViewRepository repository)
{
    public List<EmployeeDetailsView> GetAll()
    {
        return repository.GetAll();
    }

    public EmployeeDetailsView? GetByEmployeeId(int employeeId)
    {
        return repository.GetByEmployeeId(employeeId);
    }

    public List<EmployeeDetailsView> GetByRestaurantId(int restaurantId)
    {
        return repository.GetByRestaurantId(restaurantId);
    }
}