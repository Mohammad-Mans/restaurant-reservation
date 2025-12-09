using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Interfaces;

public interface IEmployeeRepository
{
    Task<Employee> CreateAsync(Employee employee);
    Task<Employee?> GetByIdAsync(int id);
    Task<List<Employee>> GetAllAsync();
    Task<Employee?> UpdateAsync(Employee employee);
    Task<bool> DeleteAsync(int id);
    Task<List<Employee>> ListManagersAsync();
    Task<Employee?> GetByUsernameAsync(string username);
}