using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class OrderItemService(IOrderItemRepository repository)
{
    public OrderItem CreateOrderItem(int orderId, int itemId, int quantity)
    {
        var orderItem = new OrderItem
        {
            OrderId = orderId,
            ItemId = itemId,
            Quantity = quantity
        };
        return repository.Create(orderItem);
    }

    public OrderItem? UpdateOrderItem(int id, int? orderId = null, int? itemId = null, int? quantity = null)
    {
        var orderItem = repository.GetById(id);
        if (orderItem == null) return null;

        if (orderId.HasValue) orderItem.OrderId = orderId.Value;
        if (itemId.HasValue) orderItem.ItemId = itemId.Value;
        if (quantity.HasValue) orderItem.Quantity = quantity.Value;

        return repository.Update(orderItem);
    }

    public bool DeleteOrderItem(int id)
    {
        return repository.Delete(id);
    }
}