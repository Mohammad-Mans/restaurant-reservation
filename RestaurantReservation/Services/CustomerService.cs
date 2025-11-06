using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class CustomerService(ICustomerRepository repository)
{
    public async Task<Customer> CreateCustomerAsync(string firstName, string lastName, string? email = null, string? phoneNumber = null)
    {
        var customer = new Customer
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber
        };
        return await repository.CreateAsync(customer);
    }

    public async Task<Customer?> UpdateCustomerAsync(int id, string? firstName = null, string? lastName = null, string? email = null,
        string? phoneNumber = null)
    {
        var customer = await repository.GetByIdAsync(id);
        if (customer == null) return null;

        if (firstName != null) customer.FirstName = firstName;
        if (lastName != null) customer.LastName = lastName;
        if (email != null) customer.Email = email;
        if (phoneNumber != null) customer.PhoneNumber = phoneNumber;

        return await repository.UpdateAsync(customer);
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        return await repository.DeleteAsync(id);
    }

    public async Task<List<Customer>> FindCustomersByPartySizeAsync(int minPartySize)
    {
        return await repository.FindCustomersByPartySizeAsync(minPartySize);
    }
}