using RestaurantReservation.API.Dtos;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.Mappings;

public static class EmployeeMappings
{
    public static EmployeeResponseDto ToDto(this Employee employee) =>
        new(
            employee.EmployeeId,
            employee.RestaurantId,
            employee.FirstName,
            employee.LastName,
            employee.Position
        );
}