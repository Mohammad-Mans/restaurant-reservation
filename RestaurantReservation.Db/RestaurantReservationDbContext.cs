using Microsoft.EntityFrameworkCore;

namespace RestaurantReservation.Db;

public class RestaurantReservationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = RestaurantReservationCore"
        );
    }
}