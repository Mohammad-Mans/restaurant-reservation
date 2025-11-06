using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class MenuItemRepository(RestaurantReservationDbContext context) : IMenuItemRepository
{
    public async Task<MenuItem> CreateAsync(MenuItem menuItem)
    {
        context.MenuItems.Add(menuItem);
        await context.SaveChangesAsync();
        return menuItem;
    }

    public async Task<MenuItem?> GetByIdAsync(int id)
    {
        return await context.MenuItems.FindAsync(id);
    }

    public async Task<List<MenuItem>> GetAllAsync()
    {
        return await context.MenuItems.ToListAsync();
    }

    public async Task<MenuItem?> UpdateAsync(MenuItem menuItem)
    {
        var existing = await context.MenuItems.FindAsync(menuItem.ItemId);
        if (existing == null) return null;

        existing.RestaurantId = menuItem.RestaurantId;
        existing.Name = menuItem.Name;
        existing.Description = menuItem.Description;
        existing.Price = menuItem.Price;

        await context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var menuItem = await context.MenuItems.FindAsync(id);
        if (menuItem == null) return false;

        context.MenuItems.Remove(menuItem);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<List<MenuItem>> GetByReservationIdAsync(int reservationId)
    {
        return await context.OrderItems
            .Where(oi => oi.Order.ReservationId == reservationId)
            .Select(oi => oi.Item)
            .ToListAsync();
    }
}