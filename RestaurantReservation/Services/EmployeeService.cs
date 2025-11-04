using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class EmployeeService(IEmployeeRepository repository)
{
    public Employee CreateEmployee(int restaurantId, string firstName, string lastName, string position)
    {
        var employee = new Employee
        {
            RestaurantId = restaurantId,
            FirstName = firstName,
            LastName = lastName,
            Position = position
        };
        return repository.Create(employee);
    }

    public Employee? UpdateEmployee(int id, int? restaurantId = null, string? firstName = null, string? lastName = null,
        string? position = null)
    {
        var employee = repository.GetById(id);
        if (employee == null) return null;

        if (restaurantId.HasValue) employee.RestaurantId = restaurantId.Value;
        if (firstName != null) employee.FirstName = firstName;
        if (lastName != null) employee.LastName = lastName;
        if (position != null) employee.Position = position;

        return repository.Update(employee);
    }

    public bool DeleteEmployee(int id)
    {
        return repository.Delete(id);
    }

    public List<Employee> ListManagers()
    {
        return repository.ListManagers();
    }
}