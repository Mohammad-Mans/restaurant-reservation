using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Configurations;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db;

public class RestaurantReservationDbContext : DbContext
{
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Table> Tables { get; set; }

    public DbSet<ReservationDetailsView> ReservationDetailsView { get; set; }
    public DbSet<EmployeeDetailsView> EmployeeDetailsView { get; set; }

    public RestaurantReservationDbContext(DbContextOptions<RestaurantReservationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new MenuItemConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        modelBuilder.ApplyConfiguration(new ReservationConfiguration());
        modelBuilder.ApplyConfiguration(new RestaurantConfiguration());
        modelBuilder.ApplyConfiguration(new TableConfiguration());
        modelBuilder.ApplyConfiguration(new ReservationDetailsViewConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeDetailsViewConfiguration());

        modelBuilder.HasDbFunction(
                typeof(DbFunctions).GetMethod(
                    nameof(DbFunctions.CalculateRestaurantRevenue),
                    new[] { typeof(int) })!)
            .HasName("CalculateRestaurantRevenue")
            .HasSchema("dbo");
    }
}