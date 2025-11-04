using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class EmployeeRepository(RestaurantReservationDbContext context) : IEmployeeRepository
{
    public Employee Create(Employee employee)
    {
        context.Employees.Add(employee);
        context.SaveChanges();
        return employee;
    }

    public Employee? GetById(int id)
    {
        return context.Employees.Find(id);
    }

    public List<Employee> GetAll()
    {
        return context.Employees.ToList();
    }

    public Employee? Update(Employee employee)
    {
        var existing = context.Employees.Find(employee.EmployeeId);
        if (existing == null) return null;

        existing.RestaurantId = employee.RestaurantId;
        existing.FirstName = employee.FirstName;
        existing.LastName = employee.LastName;
        existing.Position = employee.Position;

        context.SaveChanges();
        return existing;
    }

    public bool Delete(int id)
    {
        var employee = context.Employees.Find(id);
        if (employee == null) return false;

        context.Employees.Remove(employee);
        context.SaveChanges();
        return true;
    }

    public List<Employee> ListManagers()
    {
        return context.Employees
            .Where(e => e.Position.ToLower() == "manager".ToLower())
            .ToList();
    }
}