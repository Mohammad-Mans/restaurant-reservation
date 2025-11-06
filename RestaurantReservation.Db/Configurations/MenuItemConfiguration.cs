using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Configurations;

public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.HasKey(m => m.ItemId);

        builder.HasOne(m => m.Restaurant)
            .WithMany(r => r.MenuItems)
            .HasForeignKey(m => m.RestaurantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new MenuItem { ItemId = 1, RestaurantId = 1, Name = "Spaghetti Carbonara", Description = "Creamy pasta with bacon and parmesan", Price = 18.99m },
            new MenuItem { ItemId = 2, RestaurantId = 1, Name = "Margherita Pizza", Description = "Classic pizza with tomato, mozzarella, and basil", Price = 16.50m },
            new MenuItem { ItemId = 3, RestaurantId = 2, Name = "Grilled Salmon", Description = "Fresh Atlantic salmon with lemon butter sauce", Price = 24.99m },
            new MenuItem { ItemId = 4, RestaurantId = 2, Name = "Lobster Roll", Description = "Fresh lobster meat on toasted brioche bun", Price = 29.99m },
            new MenuItem { ItemId = 5, RestaurantId = 3, Name = "Ribeye Steak", Description = "12oz prime ribeye with garlic mashed potatoes", Price = 35.99m }
        );
    }
}