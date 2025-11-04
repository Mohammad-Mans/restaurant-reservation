using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Interfaces;

public interface ITableRepository
{
    Table Create(Table table);
    Table? GetById(int id);
    List<Table> GetAll();
    Table? Update(Table table);
    bool Delete(int id);
}