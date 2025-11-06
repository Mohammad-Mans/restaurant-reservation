using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Configurations;

public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.HasData(
            new Restaurant { RestaurantId = 1, Name = "Bella Vista Italian", Address = "123 Main Street, Downtown", PhoneNumber = "555-0101", OpeningHours = "11:00 AM - 10:00 PM" },
            new Restaurant { RestaurantId = 2, Name = "Ocean Breeze Seafood", Address = "456 Harbor Drive, Waterfront", PhoneNumber = "555-0102", OpeningHours = "12:00 PM - 11:00 PM" },
            new Restaurant { RestaurantId = 3, Name = "Grill Masters Steakhouse", Address = "789 Oak Avenue, Uptown", PhoneNumber = "555-0103", OpeningHours = "5:00 PM - 12:00 AM" },
            new Restaurant { RestaurantId = 4, Name = "Golden Dragon Asian Fusion", Address = "321 Pine Street, Chinatown", PhoneNumber = "555-0104", OpeningHours = "11:30 AM - 10:30 PM" },
            new Restaurant { RestaurantId = 5, Name = "Le Café Parisien", Address = "654 Elm Boulevard, French Quarter", PhoneNumber = "555-0105", OpeningHours = "8:00 AM - 9:00 PM" }
        );
    }
}