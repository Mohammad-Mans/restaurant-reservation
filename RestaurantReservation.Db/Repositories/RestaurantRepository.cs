using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class RestaurantRepository(RestaurantReservationDbContext context) : IRestaurantRepository
{
    public Restaurant Create(Restaurant restaurant)
    {
        context.Restaurants.Add(restaurant);
        context.SaveChanges();
        return restaurant;
    }

    public Restaurant? GetById(int id)
    {
        return context.Restaurants.Find(id);
    }

    public IEnumerable<Restaurant> GetAll()
    {
        return context.Restaurants.ToList();
    }

    public Restaurant? Update(Restaurant restaurant)
    {
        var existing = context.Restaurants.Find(restaurant.RestaurantId);
        if (existing == null) return null;

        existing.Name = restaurant.Name;
        existing.Address = restaurant.Address;
        existing.PhoneNumber = restaurant.PhoneNumber;
        existing.OpeningHours = restaurant.OpeningHours;

        context.SaveChanges();
        return existing;
    }

    public bool Delete(int id)
    {
        var restaurant = context.Restaurants.Find(id);
        if (restaurant == null) return false;

        context.Restaurants.Remove(restaurant);
        context.SaveChanges();
        return true;
    }
}