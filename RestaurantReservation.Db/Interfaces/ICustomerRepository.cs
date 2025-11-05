using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Interfaces;

public interface ICustomerRepository
{
    Customer Create(Customer customer);
    Customer? GetById(int id);
    List<Customer> GetAll();
    Customer? Update(Customer customer);
    bool Delete(int id);
    List<Customer> FindCustomersByPartySize(int minPartySize);
}