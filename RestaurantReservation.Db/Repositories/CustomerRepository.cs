using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class CustomerRepository(RestaurantReservationDbContext context) : ICustomerRepository
{
    public Customer Create(Customer customer)
    {
        context.Customers.Add(customer);
        context.SaveChanges();
        return customer;
    }

    public Customer? GetById(int id)
    {
        return context.Customers.Find(id);
    }

    public List<Customer> GetAll()
    {
        return context.Customers.ToList();
    }

    public Customer? Update(Customer customer)
    {
        var existing = context.Customers.Find(customer.CustomerId);
        if (existing == null) return null;

        existing.FirstName = customer.FirstName;
        existing.LastName = customer.LastName;
        existing.Email = customer.Email;
        existing.PhoneNumber = customer.PhoneNumber;

        context.SaveChanges();
        return existing;
    }

    public bool Delete(int id)
    {
        var customer = context.Customers.Find(id);
        if (customer == null) return false;

        context.Customers.Remove(customer);
        context.SaveChanges();
        return true;
    }
}