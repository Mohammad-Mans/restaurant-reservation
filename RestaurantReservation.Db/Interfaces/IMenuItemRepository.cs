using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Interfaces;

public interface IMenuItemRepository
{
    MenuItem Create(MenuItem menuItem);
    MenuItem? GetById(int id);
    IEnumerable<MenuItem> GetAll();
    MenuItem? Update(MenuItem menuItem);
    bool Delete(int id);
}