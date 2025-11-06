using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Interfaces;

public interface ITableRepository
{
    Task<Table> CreateAsync(Table table);
    Task<Table?> GetByIdAsync(int id);
    Task<List<Table>> GetAllAsync();
    Task<Table?> UpdateAsync(Table table);
    Task<bool> DeleteAsync(int id);
}