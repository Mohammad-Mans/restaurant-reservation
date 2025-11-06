using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasData(
            new Customer { CustomerId = 1, FirstName = "John", LastName = "Smith", Email = "john.smith@email.com", PhoneNumber = "555-1001" },
            new Customer { CustomerId = 2, FirstName = "Emily", LastName = "Johnson", Email = "emily.j@email.com", PhoneNumber = "555-1002" },
            new Customer { CustomerId = 3, FirstName = "Michael", LastName = "Williams", Email = "michael.w@email.com", PhoneNumber = "555-1003" },
            new Customer { CustomerId = 4, FirstName = "Sarah", LastName = "Brown", Email = "sarah.brown@email.com", PhoneNumber = "555-1004" },
            new Customer { CustomerId = 5, FirstName = "David", LastName = "Davis", Email = "david.d@email.com", PhoneNumber = "555-1005" }
        );
    }
}