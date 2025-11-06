using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class OrderItemRepository(RestaurantReservationDbContext context) : IOrderItemRepository
{
    public async Task<OrderItem> CreateAsync(OrderItem orderItem)
    {
        context.OrderItems.Add(orderItem);
        await context.SaveChangesAsync();
        return orderItem;
    }

    public async Task<OrderItem?> GetByIdAsync(int id)
    {
        return await context.OrderItems.FindAsync(id);
    }

    public async Task<List<OrderItem>> GetAllAsync()
    {
        return await context.OrderItems.ToListAsync();
    }

    public async Task<OrderItem?> UpdateAsync(OrderItem orderItem)
    {
        var existing = await context.OrderItems.FindAsync(orderItem.OrderItemId);
        if (existing == null) return null;

        existing.OrderId = orderItem.OrderId;
        existing.ItemId = orderItem.ItemId;
        existing.Quantity = orderItem.Quantity;

        await context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var orderItem = await context.OrderItems.FindAsync(id);
        if (orderItem == null) return false;

        context.OrderItems.Remove(orderItem);
        await context.SaveChangesAsync();
        return true;
    }
}