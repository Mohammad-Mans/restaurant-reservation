using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class EmployeeService(IEmployeeRepository repository)
{
    public async Task<Employee> CreateEmployeeAsync(int restaurantId, string firstName, string lastName, string position)
    {
        var employee = new Employee
        {
            RestaurantId = restaurantId,
            FirstName = firstName,
            LastName = lastName,
            Position = position
        };
        return await repository.CreateAsync(employee);
    }

    public async Task<Employee?> UpdateEmployeeAsync(int id, int? restaurantId = null, string? firstName = null, string? lastName = null,
        string? position = null)
    {
        var employee = await repository.GetByIdAsync(id);
        if (employee == null) return null;

        if (restaurantId.HasValue) employee.RestaurantId = restaurantId.Value;
        if (firstName != null) employee.FirstName = firstName;
        if (lastName != null) employee.LastName = lastName;
        if (position != null) employee.Position = position;

        return await repository.UpdateAsync(employee);
    }

    public async Task<bool> DeleteEmployeeAsync(int id)
    {
        return await repository.DeleteAsync(id);
    }

    public async Task<List<Employee>> ListManagersAsync()
    {
        return await repository.ListManagersAsync();
    }
}