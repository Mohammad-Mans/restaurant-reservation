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

        builder.HasData(
            new Employee { EmployeeId = 1, RestaurantId = 1, FirstName = "Alice", LastName = "Martinez", Position = "Head Chef" },
            new Employee { EmployeeId = 2, RestaurantId = 1, FirstName = "Bob", LastName = "Thompson", Position = "Waiter" },
            new Employee { EmployeeId = 3, RestaurantId = 2, FirstName = "Carol", LastName = "Anderson", Position = "Manager" },
            new Employee { EmployeeId = 4, RestaurantId = 2, FirstName = "Daniel", LastName = "Wilson", Position = "Sous Chef" },
            new Employee { EmployeeId = 5, RestaurantId = 3, FirstName = "Emma", LastName = "Taylor", Position = "Hostess" }
        );
    }
}