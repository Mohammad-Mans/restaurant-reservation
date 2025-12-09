using RestaurantReservation.API.Dtos;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.Mappings;

public static class MenuItemMappings
{
    public static MenuItemResponseDto ToDto(this MenuItem menuItem) =>
        new(
            menuItem.ItemId,
            menuItem.RestaurantId,
            menuItem.Name,
            menuItem.Description,
            menuItem.Price
        );
}