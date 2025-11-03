using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class RestaurantService(IRestaurantRepository repository)
{
    public Restaurant CreateRestaurant(string name, string? address = null, string? phoneNumber = null,
        string? openingHours = null)
    {
        var restaurant = new Restaurant
        {
            Name = name,
            Address = address,
            PhoneNumber = phoneNumber,
            OpeningHours = openingHours
        };
        return repository.Create(restaurant);
    }

    public Restaurant? UpdateRestaurant(int id, string? name = null, string? address = null, string? phoneNumber = null,
        string? openingHours = null)
    {
        var restaurant = repository.GetById(id);
        if (restaurant == null) return null;

        if (name != null) restaurant.Name = name;
        if (address != null) restaurant.Address = address;
        if (phoneNumber != null) restaurant.PhoneNumber = phoneNumber;
        if (openingHours != null) restaurant.OpeningHours = openingHours;

        return repository.Update(restaurant);
    }

    public bool DeleteRestaurant(int id)
    {
        return repository.Delete(id);
    }
}