using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class MenuItemRepository(RestaurantReservationDbContext context) : IMenuItemRepository
{
    public MenuItem Create(MenuItem menuItem)
    {
        context.MenuItems.Add(menuItem);
        context.SaveChanges();
        return menuItem;
    }

    public MenuItem? GetById(int id)
    {
        return context.MenuItems.Find(id);
    }

    public List<MenuItem> GetAll()
    {
        return context.MenuItems.ToList();
    }

    public MenuItem? Update(MenuItem menuItem)
    {
        var existing = context.MenuItems.Find(menuItem.ItemId);
        if (existing == null) return null;

        existing.RestaurantId = menuItem.RestaurantId;
        existing.Name = menuItem.Name;
        existing.Description = menuItem.Description;
        existing.Price = menuItem.Price;

        context.SaveChanges();
        return existing;
    }

    public bool Delete(int id)
    {
        var menuItem = context.MenuItems.Find(id);
        if (menuItem == null) return false;

        context.MenuItems.Remove(menuItem);
        context.SaveChanges();
        return true;
    }
}