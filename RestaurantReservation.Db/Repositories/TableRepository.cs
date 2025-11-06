using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class TableRepository(RestaurantReservationDbContext context) : ITableRepository
{
    public async Task<Table> CreateAsync(Table table)
    {
        context.Tables.Add(table);
        await context.SaveChangesAsync();
        return table;
    }

    public async Task<Table?> GetByIdAsync(int id)
    {
        return await context.Tables.FindAsync(id);
    }

    public async Task<List<Table>> GetAllAsync()
    {
        return await context.Tables.ToListAsync();
    }

    public async Task<Table?> UpdateAsync(Table table)
    {
        var existing = await context.Tables.FindAsync(table.TableId);
        if (existing == null) return null;

        existing.RestaurantId = table.RestaurantId;
        existing.Capacity = table.Capacity;

        await context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var table = await context.Tables.FindAsync(id);
        if (table == null) return false;

        context.Tables.Remove(table);
        await context.SaveChangesAsync();
        return true;
    }
}