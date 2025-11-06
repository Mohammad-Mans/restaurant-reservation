using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class RestaurantService(IRestaurantRepository repository)
{
    public async Task<Restaurant> CreateRestaurantAsync(string name, string? address = null, string? phoneNumber = null,
        string? openingHours = null)
    {
        var restaurant = new Restaurant
        {
            Name = name,
            Address = address,
            PhoneNumber = phoneNumber,
            OpeningHours = openingHours
        };
        return await repository.CreateAsync(restaurant);
    }

    public async Task<Restaurant?> UpdateRestaurantAsync(int id, string? name = null, string? address = null, string? phoneNumber = null,
        string? openingHours = null)
    {
        var restaurant = await repository.GetByIdAsync(id);
        if (restaurant == null) return null;

        if (name != null) restaurant.Name = name;
        if (address != null) restaurant.Address = address;
        if (phoneNumber != null) restaurant.PhoneNumber = phoneNumber;
        if (openingHours != null) restaurant.OpeningHours = openingHours;

        return await repository.UpdateAsync(restaurant);
    }

    public async Task<bool> DeleteRestaurantAsync(int id)
    {
        return await repository.DeleteAsync(id);
    }

    public async Task<Restaurant?> GetRestaurantByIdAsync(int id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task<decimal> CalculateRestaurantRevenueAsync(int restaurantId)
    {
        return await repository.CalculateRestaurantRevenueAsync(restaurantId);
    }
}