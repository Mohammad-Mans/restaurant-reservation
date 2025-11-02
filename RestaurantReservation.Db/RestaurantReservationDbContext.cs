using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = RestaurantReservationCore"
        ).LogTo(Console.WriteLine,
            new[] { DbLoggerCategory.Database.Command.Name },
            LogLevel.Information)
        .EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Reservation)
            .WithMany(r => r.Orders)
            .HasForeignKey(o => o.ReservationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Employee)
            .WithMany(e => e.Orders)
            .HasForeignKey(o => o.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Customer)
            .WithMany(c => c.Reservations)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Restaurant)
            .WithMany(rest => rest.Reservations)
            .HasForeignKey(r => r.RestaurantId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Table)
            .WithMany(t => t.Reservations)
            .HasForeignKey(r => r.TableId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Table>()
            .HasOne(t => t.Restaurant)
            .WithMany(r => r.Tables)
            .HasForeignKey(t => t.RestaurantId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Restaurant)
            .WithMany(r => r.Employees)
            .HasForeignKey(e => e.RestaurantId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MenuItem>()
            .HasOne(m => m.Restaurant)
            .WithMany(r => r.MenuItems)
            .HasForeignKey(m => m.RestaurantId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Item)
            .WithMany(mi => mi.OrderItems)
            .HasForeignKey(oi => oi.ItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}