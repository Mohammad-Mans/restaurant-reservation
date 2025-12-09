using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class EmployeeRepository(RestaurantReservationDbContext context) : IEmployeeRepository
{
    public async Task<Employee> CreateAsync(Employee employee)
    {
        context.Employees.Add(employee);
        await context.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await context.Employees.FindAsync(id);
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        return await context.Employees.ToListAsync();
    }

    public async Task<Employee?> UpdateAsync(Employee employee)
    {
        var existing = await context.Employees.FindAsync(employee.EmployeeId);
        if (existing == null) return null;

        existing.RestaurantId = employee.RestaurantId;
        existing.FirstName = employee.FirstName;
        existing.LastName = employee.LastName;
        existing.Position = employee.Position;

        await context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var employee = await context.Employees.FindAsync(id);
        if (employee == null) return false;

        context.Employees.Remove(employee);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Employee>> ListManagersAsync()
    {
        return await context.Employees
            .Where(e => e.Position.ToLower() == "manager".ToLower())
            .ToListAsync();
    }

    public async Task<Employee?> GetByUsernameAsync(string username)
    {
        return await context.Employees
            .FirstOrDefaultAsync(e => e.Username == username);
    }
}