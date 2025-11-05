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

    public DbSet<ReservationDetailsView> ReservationDetailsView { get; set; }
    public DbSet<EmployeeDetailsView> EmployeeDetailsView { get; set; }

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

        modelBuilder.Entity<MenuItem>()
            .HasKey(m => m.ItemId);

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

        modelBuilder.Entity<Restaurant>().HasData(
            new Restaurant { RestaurantId = 1, Name = "Bella Vista Italian", Address = "123 Main Street, Downtown", PhoneNumber = "555-0101", OpeningHours = "11:00 AM - 10:00 PM" },
            new Restaurant { RestaurantId = 2, Name = "Ocean Breeze Seafood", Address = "456 Harbor Drive, Waterfront", PhoneNumber = "555-0102", OpeningHours = "12:00 PM - 11:00 PM" },
            new Restaurant { RestaurantId = 3, Name = "Grill Masters Steakhouse", Address = "789 Oak Avenue, Uptown", PhoneNumber = "555-0103", OpeningHours = "5:00 PM - 12:00 AM" },
            new Restaurant { RestaurantId = 4, Name = "Golden Dragon Asian Fusion", Address = "321 Pine Street, Chinatown", PhoneNumber = "555-0104", OpeningHours = "11:30 AM - 10:30 PM" },
            new Restaurant { RestaurantId = 5, Name = "Le Café Parisien", Address = "654 Elm Boulevard, French Quarter", PhoneNumber = "555-0105", OpeningHours = "8:00 AM - 9:00 PM" }
        );

        modelBuilder.Entity<Customer>().HasData(
            new Customer { CustomerId = 1, FirstName = "John", LastName = "Smith", Email = "john.smith@email.com", PhoneNumber = "555-1001" },
            new Customer { CustomerId = 2, FirstName = "Emily", LastName = "Johnson", Email = "emily.j@email.com", PhoneNumber = "555-1002" },
            new Customer { CustomerId = 3, FirstName = "Michael", LastName = "Williams", Email = "michael.w@email.com", PhoneNumber = "555-1003" },
            new Customer { CustomerId = 4, FirstName = "Sarah", LastName = "Brown", Email = "sarah.brown@email.com", PhoneNumber = "555-1004" },
            new Customer { CustomerId = 5, FirstName = "David", LastName = "Davis", Email = "david.d@email.com", PhoneNumber = "555-1005" }
        );

        modelBuilder.Entity<Employee>().HasData(
            new Employee { EmployeeId = 1, RestaurantId = 1, FirstName = "Alice", LastName = "Martinez", Position = "Head Chef" },
            new Employee { EmployeeId = 2, RestaurantId = 1, FirstName = "Bob", LastName = "Thompson", Position = "Waiter" },
            new Employee { EmployeeId = 3, RestaurantId = 2, FirstName = "Carol", LastName = "Anderson", Position = "Manager" },
            new Employee { EmployeeId = 4, RestaurantId = 2, FirstName = "Daniel", LastName = "Wilson", Position = "Sous Chef" },
            new Employee { EmployeeId = 5, RestaurantId = 3, FirstName = "Emma", LastName = "Taylor", Position = "Hostess" }
        );

        modelBuilder.Entity<Table>().HasData(
            new Table { TableId = 1, RestaurantId = 1, Capacity = 2 },
            new Table { TableId = 2, RestaurantId = 1, Capacity = 4 },
            new Table { TableId = 3, RestaurantId = 2, Capacity = 6 },
            new Table { TableId = 4, RestaurantId = 2, Capacity = 2 },
            new Table { TableId = 5, RestaurantId = 3, Capacity = 8 }
        );

        modelBuilder.Entity<MenuItem>().HasData(
            new MenuItem { ItemId = 1, RestaurantId = 1, Name = "Spaghetti Carbonara", Description = "Creamy pasta with bacon and parmesan", Price = 18.99m },
            new MenuItem { ItemId = 2, RestaurantId = 1, Name = "Margherita Pizza", Description = "Classic pizza with tomato, mozzarella, and basil", Price = 16.50m },
            new MenuItem { ItemId = 3, RestaurantId = 2, Name = "Grilled Salmon", Description = "Fresh Atlantic salmon with lemon butter sauce", Price = 24.99m },
            new MenuItem { ItemId = 4, RestaurantId = 2, Name = "Lobster Roll", Description = "Fresh lobster meat on toasted brioche bun", Price = 29.99m },
            new MenuItem { ItemId = 5, RestaurantId = 3, Name = "Ribeye Steak", Description = "12oz prime ribeye with garlic mashed potatoes", Price = 35.99m }
        );

        modelBuilder.Entity<Reservation>().HasData(
            new Reservation { ReservationId = 1, CustomerId = 1, RestaurantId = 1, TableId = 1, ReservationDate = new DateTime(2025, 11, 15, 19, 0, 0), PartySize = 2 },
            new Reservation { ReservationId = 2, CustomerId = 2, RestaurantId = 1, TableId = 2, ReservationDate = new DateTime(2025, 11, 16, 20, 0, 0), PartySize = 4 },
            new Reservation { ReservationId = 3, CustomerId = 3, RestaurantId = 2, TableId = 3, ReservationDate = new DateTime(2025, 11, 17, 18, 30, 0), PartySize = 6 },
            new Reservation { ReservationId = 4, CustomerId = 4, RestaurantId = 2, TableId = 4, ReservationDate = new DateTime(2025, 11, 18, 19, 30, 0), PartySize = 2 },
            new Reservation { ReservationId = 5, CustomerId = 5, RestaurantId = 3, TableId = 5, ReservationDate = new DateTime(2025, 11, 19, 20, 0, 0), PartySize = 8 }
        );

        modelBuilder.Entity<Order>().HasData(
            new Order { OrderId = 1, ReservationId = 1, EmployeeId = 2, OrderDate = new DateTime(2025, 11, 15, 19, 15, 0), TotalAmount = 35.49m },
            new Order { OrderId = 2, ReservationId = 2, EmployeeId = 2, OrderDate = new DateTime(2025, 11, 16, 20, 10, 0), TotalAmount = 82.50m },
            new Order { OrderId = 3, ReservationId = 3, EmployeeId = 3, OrderDate = new DateTime(2025, 11, 17, 18, 45, 0), TotalAmount = 149.94m },
            new Order { OrderId = 4, ReservationId = 4, EmployeeId = 3, OrderDate = new DateTime(2025, 11, 18, 19, 45, 0), TotalAmount = 29.99m },
            new Order { OrderId = 5, ReservationId = 5, EmployeeId = 5, OrderDate = new DateTime(2025, 11, 19, 20, 15, 0), TotalAmount = 287.92m }
        );

        modelBuilder.Entity<OrderItem>().HasData(
            new OrderItem { OrderItemId = 1, OrderId = 1, ItemId = 1, Quantity = 1 },
            new OrderItem { OrderItemId = 2, OrderId = 1, ItemId = 2, Quantity = 1 },
            new OrderItem { OrderItemId = 3, OrderId = 2, ItemId = 2, Quantity = 5 },
            new OrderItem { OrderItemId = 4, OrderId = 3, ItemId = 3, Quantity = 6 },
            new OrderItem { OrderItemId = 5, OrderId = 4, ItemId = 4, Quantity = 1 }
        );

        modelBuilder.Entity<ReservationDetailsView>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("vw_ReservationDetails");
        });

        modelBuilder.Entity<EmployeeDetailsView>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("vw_EmployeeDetails");
        });

        modelBuilder.HasDbFunction(
                typeof(DbFunctions).GetMethod(
                    nameof(DbFunctions.CalculateRestaurantRevenue),
                    new[] { typeof(int) })!)
            .HasName("CalculateRestaurantRevenue")
            .HasSchema("dbo");
    }
}