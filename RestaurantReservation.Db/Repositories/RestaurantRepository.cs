using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class RestaurantRepository(RestaurantReservationDbContext context) : IRestaurantRepository
{
    public async Task<Restaurant> CreateAsync(Restaurant restaurant)
    {
        context.Restaurants.Add(restaurant);
        await context.SaveChangesAsync();
        return restaurant;
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        return await context.Restaurants.FindAsync(id);
    }

    public async Task<List<Restaurant>> GetAllAsync()
    {
        return await context.Restaurants.ToListAsync();
    }

    public async Task<Restaurant?> UpdateAsync(Restaurant restaurant)
    {
        var existing = await context.Restaurants.FindAsync(restaurant.RestaurantId);
        if (existing == null) return null;

        existing.Name = restaurant.Name;
        existing.Address = restaurant.Address;
        existing.PhoneNumber = restaurant.PhoneNumber;
        existing.OpeningHours = restaurant.OpeningHours;

        await context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var restaurant = await context.Restaurants.FindAsync(id);
        if (restaurant == null) return false;

        context.Restaurants.Remove(restaurant);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<decimal> CalculateRestaurantRevenueAsync(int restaurantId)
    {
        return await context.Database
            .SqlQueryRaw<decimal>("SELECT dbo.CalculateRestaurantRevenue({0}) AS Value", restaurantId)
            .FirstOrDefaultAsync();
    }
}