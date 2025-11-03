using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class TableService(ITableRepository repository)
{
    public Table CreateTable(int restaurantId, int capacity)
    {
        var table = new Table
        {
            RestaurantId = restaurantId,
            Capacity = capacity
        };
        return repository.Create(table);
    }

    public Table? UpdateTable(int id, int? restaurantId = null, int? capacity = null)
    {
        var table = repository.GetById(id);
        if (table == null) return null;

        if (restaurantId.HasValue) table.RestaurantId = restaurantId.Value;
        if (capacity.HasValue) table.Capacity = capacity.Value;

        return repository.Update(table);
    }

    public bool DeleteTable(int id)
    {
        return repository.Delete(id);
    }
}