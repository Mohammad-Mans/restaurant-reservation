using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class OrderItemRepository(RestaurantReservationDbContext context) : IOrderItemRepository
{
    public OrderItem Create(OrderItem orderItem)
    {
        context.OrderItems.Add(orderItem);
        context.SaveChanges();
        return orderItem;
    }

    public OrderItem? GetById(int id)
    {
        return context.OrderItems.Find(id);
    }

    public IEnumerable<OrderItem> GetAll()
    {
        return context.OrderItems.ToList();
    }

    public OrderItem? Update(OrderItem orderItem)
    {
        var existing = context.OrderItems.Find(orderItem.OrderItemId);
        if (existing == null) return null;

        existing.OrderId = orderItem.OrderId;
        existing.ItemId = orderItem.ItemId;
        existing.Quantity = orderItem.Quantity;

        context.SaveChanges();
        return existing;
    }

    public bool Delete(int id)
    {
        var orderItem = context.OrderItems.Find(id);
        if (orderItem == null) return false;

        context.OrderItems.Remove(orderItem);
        context.SaveChanges();
        return true;
    }
}