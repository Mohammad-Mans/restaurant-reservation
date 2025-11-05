using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Interfaces;

public interface IEmployeeDetailsViewRepository
{
    List<EmployeeDetailsView> GetAll();
    EmployeeDetailsView? GetByEmployeeId(int employeeId);
    List<EmployeeDetailsView> GetByRestaurantId(int restaurantId);
}