using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Interfaces;

public interface IOrderRepository
{
    Order Create(Order order);
    Order? GetById(int id);
    List<Order> GetAll();
    Order? Update(Order order);
    bool Delete(int id);
    List<Order> GetByReservationId(int reservationId);
}