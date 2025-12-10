using RestaurantReservation.API.Constants;
using RestaurantReservation.API.Dtos;
using RestaurantReservation.API.Mappings;
using RestaurantReservation.Db.Interfaces;

namespace RestaurantReservation.API.Endpoints;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/customers").WithTags(ApiTags.Customers);

        group.MapGet("/search", async (
                string? firstName,
                string? lastName,
                string? email,
                string? phoneNumber,
                ICustomerRepository repo) =>
            {
                var customers = await repo.SearchAsync(firstName, lastName, email, phoneNumber);
                var customerDtos = customers.Select(customer => customer.ToDto()).ToList();
                return Results.Ok(customerDtos);
            })
            .RequireAuthorization(policy => policy.RequireRole(Roles.Employee))
            .Produces<List<CustomerResponseDto>>()
            .WithName("SearchCustomers")
            .WithSummary("Search customers by various criteria")
            .WithDescription(
                "Search for customers by first name, last name, email, or phone number. All parameters are optional and can be combined.");
    }
}