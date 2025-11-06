using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Interfaces;

public interface ICustomerRepository
{
    Task<Customer> CreateAsync(Customer customer);
    Task<Customer?> GetByIdAsync(int id);
    Task<List<Customer>> GetAllAsync();
    Task<Customer?> UpdateAsync(Customer customer);
    Task<bool> DeleteAsync(int id);
    Task<List<Customer>> FindCustomersByPartySizeAsync(int minPartySize);
}