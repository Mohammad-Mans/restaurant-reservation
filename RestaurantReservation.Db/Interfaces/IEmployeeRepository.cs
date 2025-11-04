using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Interfaces;

public interface IEmployeeRepository
{
    Employee Create(Employee employee);
    Employee? GetById(int id);
    List<Employee> GetAll();
    Employee? Update(Employee employee);
    bool Delete(int id);
    List<Employee> ListManagers();
}