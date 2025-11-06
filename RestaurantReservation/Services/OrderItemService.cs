using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class OrderItemService(IOrderItemRepository repository)
{
    public async Task<OrderItem> CreateOrderItemAsync(int orderId, int itemId, int quantity)
    {
        var orderItem = new OrderItem
        {
            OrderId = orderId,
            ItemId = itemId,
            Quantity = quantity
        };
        return await repository.CreateAsync(orderItem);
    }

    public async Task<OrderItem?> UpdateOrderItemAsync(int id, int? orderId = null, int? itemId = null, int? quantity = null)
    {
        var orderItem = await repository.GetByIdAsync(id);
        if (orderItem == null) return null;

        if (orderId.HasValue) orderItem.OrderId = orderId.Value;
        if (itemId.HasValue) orderItem.ItemId = itemId.Value;
        if (quantity.HasValue) orderItem.Quantity = quantity.Value;

        return await repository.UpdateAsync(orderItem);
    }

    public async Task<bool> DeleteOrderItemAsync(int id)
    {
        return await repository.DeleteAsync(id);
    }
}