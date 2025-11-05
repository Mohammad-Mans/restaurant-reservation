using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class CustomerService(ICustomerRepository repository)
{
    public Customer CreateCustomer(string firstName, string lastName, string? email = null, string? phoneNumber = null)
    {
        var customer = new Customer
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber
        };
        return repository.Create(customer);
    }

    public Customer? UpdateCustomer(int id, string? firstName = null, string? lastName = null, string? email = null,
        string? phoneNumber = null)
    {
        var customer = repository.GetById(id);
        if (customer == null) return null;

        if (firstName != null) customer.FirstName = firstName;
        if (lastName != null) customer.LastName = lastName;
        if (email != null) customer.Email = email;
        if (phoneNumber != null) customer.PhoneNumber = phoneNumber;

        return repository.Update(customer);
    }

    public bool DeleteCustomer(int id)
    {
        return repository.Delete(id);
    }

    public List<Customer> FindCustomersByPartySize(int minPartySize)
    {
        return repository.FindCustomersByPartySize(minPartySize);
    }
}