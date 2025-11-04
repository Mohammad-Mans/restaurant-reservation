using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Interfaces;

public interface IEmployeeRepository
{
    Employee Create(Employee employee);
    Employee? GetById(int id);
    IEnumerable<Employee> GetAll();
    Employee? Update(Employee employee);
    bool Delete(int id);
    IEnumerable<Employee> ListManagers();
}