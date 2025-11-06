using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Interfaces;

public interface IRestaurantRepository
{
    Task<Restaurant> CreateAsync(Restaurant restaurant);
    Task<Restaurant?> GetByIdAsync(int id);
    Task<List<Restaurant>> GetAllAsync();
    Task<Restaurant?> UpdateAsync(Restaurant restaurant);
    Task<bool> DeleteAsync(int id);
    Task<decimal> CalculateRestaurantRevenueAsync(int restaurantId);
}