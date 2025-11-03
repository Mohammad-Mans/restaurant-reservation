using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Interfaces;

public interface IOrderItemRepository
{
    OrderItem Create(OrderItem orderItem);
    OrderItem? GetById(int id);
    IEnumerable<OrderItem> GetAll();
    OrderItem? Update(OrderItem orderItem);
    bool Delete(int id);
}