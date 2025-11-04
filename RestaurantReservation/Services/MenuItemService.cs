using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class MenuItemService(IMenuItemRepository repository)
{
    public MenuItem CreateMenuItem(int restaurantId, string name, decimal price, string? description = null)
    {
        var menuItem = new MenuItem
        {
            RestaurantId = restaurantId,
            Name = name,
            Description = description,
            Price = price
        };
        return repository.Create(menuItem);
    }

    public MenuItem? UpdateMenuItem(int id, int? restaurantId = null, string? name = null, string? description = null,
        decimal? price = null)
    {
        var menuItem = repository.GetById(id);
        if (menuItem == null) return null;

        if (restaurantId.HasValue) menuItem.RestaurantId = restaurantId.Value;
        if (name != null) menuItem.Name = name;
        if (description != null) menuItem.Description = description;
        if (price.HasValue) menuItem.Price = price.Value;

        return repository.Update(menuItem);
    }

    public bool DeleteMenuItem(int id)
    {
        return repository.Delete(id);
    }

    public List<MenuItem> ListOrderedMenuItems(int reservationId)
    {
        return repository.GetByReservationId(reservationId);
    }
}