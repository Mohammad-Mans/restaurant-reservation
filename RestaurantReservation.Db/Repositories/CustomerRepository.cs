using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class CustomerRepository(RestaurantReservationDbContext context) : ICustomerRepository
{
    public async Task<Customer> CreateAsync(Customer customer)
    {
        context.Customers.Add(customer);
        await context.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await context.Customers.FindAsync(id);
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        return await context.Customers.ToListAsync();
    }

    public async Task<Customer?> UpdateAsync(Customer customer)
    {
        var existing = await context.Customers.FindAsync(customer.CustomerId);
        if (existing == null) return null;

        existing.FirstName = customer.FirstName;
        existing.LastName = customer.LastName;
        existing.Email = customer.Email;
        existing.PhoneNumber = customer.PhoneNumber;

        await context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var customer = await context.Customers.FindAsync(id);
        if (customer == null) return false;

        context.Customers.Remove(customer);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Customer>> FindCustomersByPartySizeAsync(int minPartySize)
    {
        return await context.Customers
            .FromSqlRaw("EXEC dbo.sp_FindCustomersByPartySize {0}", minPartySize)
            .ToListAsync();
    }
}