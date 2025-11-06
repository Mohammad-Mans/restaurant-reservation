using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class TableService(ITableRepository repository)
{
    public async Task<Table> CreateTableAsync(int restaurantId, int capacity)
    {
        var table = new Table
        {
            RestaurantId = restaurantId,
            Capacity = capacity
        };
        return await repository.CreateAsync(table);
    }

    public async Task<Table?> UpdateTableAsync(int id, int? restaurantId = null, int? capacity = null)
    {
        var table = await repository.GetByIdAsync(id);
        if (table == null) return null;

        if (restaurantId.HasValue) table.RestaurantId = restaurantId.Value;
        if (capacity.HasValue) table.Capacity = capacity.Value;

        return await repository.UpdateAsync(table);
    }

    public async Task<bool> DeleteTableAsync(int id)
    {
        return await repository.DeleteAsync(id);
    }
}