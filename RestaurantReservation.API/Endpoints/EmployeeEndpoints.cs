using RestaurantReservation.API.Constants;
using RestaurantReservation.API.Dtos;
using RestaurantReservation.API.Mappings;
using RestaurantReservation.Db.Interfaces;

namespace RestaurantReservation.API.Endpoints;

public static class EmployeeEndpoints
{
    public static void MapEmployeeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/employees").WithTags(ApiTags.Employees);

        group.MapGet("/managers", async (IEmployeeRepository repo) =>
            {
                var managers = await repo.ListManagersAsync();
                var managersDtos = managers.Select(manager => manager.ToDto()).ToList();
                return Results.Ok(managersDtos);
            })
            .RequireAuthorization(policy => policy.RequireRole(Roles.Manager))
            .Produces<List<EmployeeResponseDto>>();

        group.MapGet("/{employeeId:int}/average-order-amount", async (int employeeId, IOrderRepository repo) =>
            {
                var averageAmount = await repo.GetAverageOrderAmountByEmployeeIdAsync(employeeId);
                var response = new AverageOrderAmountResponseDto(employeeId, averageAmount);
                return averageAmount.HasValue
                    ? Results.Ok(response)
                    : Results.NotFound(response);
            })
            .RequireAuthorization(policy => policy.RequireRole(Roles.Manager))
            .Produces<AverageOrderAmountResponseDto>()
            .Produces<AverageOrderAmountResponseDto>(StatusCodes.Status404NotFound);
    }
}