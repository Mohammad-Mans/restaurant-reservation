using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Interfaces;

public interface IRestaurantRepository
{
    Restaurant Create(Restaurant restaurant);
    Restaurant? GetById(int id);
    List<Restaurant> GetAll();
    Restaurant? Update(Restaurant restaurant);
    bool Delete(int id);
}