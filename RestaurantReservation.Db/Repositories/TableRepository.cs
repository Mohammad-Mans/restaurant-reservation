using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class TableRepository(RestaurantReservationDbContext context) : ITableRepository
{
    public Table Create(Table table)
    {
        context.Tables.Add(table);
        context.SaveChanges();
        return table;
    }

    public Table? GetById(int id)
    {
        return context.Tables.Find(id);
    }

    public List<Table> GetAll()
    {
        return context.Tables.ToList();
    }

    public Table? Update(Table table)
    {
        var existing = context.Tables.Find(table.TableId);
        if (existing == null) return null;

        existing.RestaurantId = table.RestaurantId;
        existing.Capacity = table.Capacity;

        context.SaveChanges();
        return existing;
    }

    public bool Delete(int id)
    {
        var table = context.Tables.Find(id);
        if (table == null) return false;

        context.Tables.Remove(table);
        context.SaveChanges();
        return true;
    }
}