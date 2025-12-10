using RestaurantReservation.API.Dtos;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.Mappings;

public static class CustomerMappings
{
    public static CustomerResponseDto ToDto(this Customer customer) =>
        new(
            customer.CustomerId,
            customer.FirstName,
            customer.LastName,
            customer.Email,
            customer.PhoneNumber
        );
}