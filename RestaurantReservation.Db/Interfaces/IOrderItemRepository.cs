using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Interfaces;

public interface IOrderItemRepository
{
    OrderItem Create(OrderItem orderItem);
    OrderItem? GetById(int id);
    List<OrderItem> GetAll();
    OrderItem? Update(OrderItem orderItem);
    bool Delete(int id);
}