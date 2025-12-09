using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasOne(e => e.Restaurant)
            .WithMany(r => r.Employees)
            .HasForeignKey(e => e.RestaurantId)
            .OnDelete(DeleteBehavior.Restrict);

        var defaultPasswordHash = BCrypt.Net.BCrypt.HashPassword("emp123");

        builder.HasData(
            new Employee
            {
                EmployeeId = 1,
                RestaurantId = 1,
                FirstName = "Alice",
                LastName = "Martinez",
                Position = "Head Chef",
                Username = "alice.martinez",
                PasswordHash = defaultPasswordHash
            },
            new Employee
            {
                EmployeeId = 2,
                RestaurantId = 1,
                FirstName = "Bob",
                LastName = "Thompson",
                Position = "Waiter",
                Username = "bob.thompson",
                PasswordHash = defaultPasswordHash
            },
            new Employee
            {
                EmployeeId = 3,
                RestaurantId = 2,
                FirstName = "Carol",
                LastName = "Anderson",
                Position = "Manager",
                Username = "carol.anderson",
                PasswordHash = defaultPasswordHash
            },
            new Employee
            {
                EmployeeId = 4,
                RestaurantId = 2,
                FirstName = "Daniel",
                LastName = "Wilson",
                Position = "Sous Chef",
                Username = "daniel.wilson",
                PasswordHash = defaultPasswordHash
            },
            new Employee
            {
                EmployeeId = 5,
                RestaurantId = 3,
                FirstName = "Emma",
                LastName = "Taylor",
                Position = "Hostess",
                Username = "emma.taylor",
                PasswordHash = defaultPasswordHash
            }
        );
    }
}