using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class OrderRepository(RestaurantReservationDbContext context) : IOrderRepository
{
    public Order Create(Order order)
    {
        context.Orders.Add(order);
        context.SaveChanges();
        return order;
    }

    public Order? GetById(int id)
    {
        return context.Orders.Find(id);
    }

    public List<Order> GetAll()
    {
        return context.Orders.ToList();
    }

    public Order? Update(Order order)
    {
        var existing = context.Orders.Find(order.OrderId);
        if (existing == null) return null;

        existing.ReservationId = order.ReservationId;
        existing.EmployeeId = order.EmployeeId;
        existing.OrderDate = order.OrderDate;
        existing.TotalAmount = order.TotalAmount;

        context.SaveChanges();
        return existing;
    }

    public bool Delete(int id)
    {
        var order = context.Orders.Find(id);
        if (order == null) return false;

        context.Orders.Remove(order);
        context.SaveChanges();
        return true;
    }
}