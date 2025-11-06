using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class MenuItemService(IMenuItemRepository repository)
{
    public async Task<MenuItem> CreateMenuItemAsync(int restaurantId, string name, decimal price, string? description = null)
    {
        var menuItem = new MenuItem
        {
            RestaurantId = restaurantId,
            Name = name,
            Description = description,
            Price = price
        };
        return await repository.CreateAsync(menuItem);
    }

    public async Task<MenuItem?> UpdateMenuItemAsync(int id, int? restaurantId = null, string? name = null, string? description = null,
        decimal? price = null)
    {
        var menuItem = await repository.GetByIdAsync(id);
        if (menuItem == null) return null;

        if (restaurantId.HasValue) menuItem.RestaurantId = restaurantId.Value;
        if (name != null) menuItem.Name = name;
        if (description != null) menuItem.Description = description;
        if (price.HasValue) menuItem.Price = price.Value;

        return await repository.UpdateAsync(menuItem);
    }

    public async Task<bool> DeleteMenuItemAsync(int id)
    {
        return await repository.DeleteAsync(id);
    }

    public async Task<List<MenuItem>> ListOrderedMenuItemsAsync(int reservationId)
    {
        return await repository.GetByReservationIdAsync(reservationId);
    }
}