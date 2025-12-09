using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        var defaultPasswordHash = BCrypt.Net.BCrypt.HashPassword("pass123");

        builder.HasData(
            new Customer
            {
                CustomerId = 1,
                FirstName = "John",
                LastName = "Smith",
                Email = "john.smith@email.com",
                PhoneNumber = "555-1001",
                Username = "johnsmith",
                PasswordHash = defaultPasswordHash
            },
            new Customer
            {
                CustomerId = 2,
                FirstName = "Emily",
                LastName = "Johnson",
                Email = "emily.j@email.com",
                PhoneNumber = "555-1002",
                Username = "emilyj",
                PasswordHash = defaultPasswordHash
            },
            new Customer
            {
                CustomerId = 3,
                FirstName = "Michael",
                LastName = "Williams",
                Email = "michael.w@email.com",
                PhoneNumber = "555-1003",
                Username = "michaelw",
                PasswordHash = defaultPasswordHash
            },
            new Customer
            {
                CustomerId = 4,
                FirstName = "Sarah",
                LastName = "Brown",
                Email = "sarah.brown@email.com",
                PhoneNumber = "555-1004",
                Username = "sarahbrown",
                PasswordHash = defaultPasswordHash
            },
            new Customer
            {
                CustomerId = 5,
                FirstName = "David",
                LastName = "Davis",
                Email = "david.d@email.com",
                PhoneNumber = "555-1005",
                Username = "davidd",
                PasswordHash = defaultPasswordHash
            }
        );
    }
}