using FluentValidation;
using RestaurantReservation.API.Auth;
using RestaurantReservation.API.Constants;
using RestaurantReservation.API.Dtos;
using RestaurantReservation.Db.Interfaces;

namespace RestaurantReservation.API.Endpoints;

public static class AuthenticationEndpoints
{
    public static void MapAuthenticationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth").WithTags(ApiTags.Authentication);

        group.MapPost("/customer/login",
                async (LoginDto login, ICustomerRepository customerRepo, JwtTokenGenerator tokenGenerator,
                    IValidator<LoginDto> validator) =>
                {
                    var validationResult = await validator.ValidateAsync(login);
                    if (!validationResult.IsValid)
                    {
                        var problemDetails = new HttpValidationProblemDetails(validationResult.ToDictionary())
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Title = "Validation failed",
                            Detail = "One or more validation errors occurred.",
                            Instance = "/api/auth/customer/login"
                        };
                        return Results.Problem(problemDetails);
                    }

                    var customer = await customerRepo.GetByUsernameAsync(login.Username);

                    if (customer == null || string.IsNullOrEmpty(customer.PasswordHash))
                        return Results.Unauthorized();

                    if (!BCrypt.Net.BCrypt.Verify(login.Password, customer.PasswordHash))
                        return Results.Unauthorized();

                    var token = tokenGenerator.GenerateToken(login.Username, customer.CustomerId, Roles.Customer);
                    var response = new LoginResponseDto(token, login.Username, Roles.Customer, customer.CustomerId);

                    return Results.Ok(response);
                })
            .Accepts<LoginDto>("application/json")
            .Produces<LoginResponseDto>()
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPost("/employee/login",
                async (LoginDto login, IEmployeeRepository employeeRepo, JwtTokenGenerator tokenGenerator,
                    IValidator<LoginDto> validator) =>
                {
                    var validationResult = await validator.ValidateAsync(login);
                    if (!validationResult.IsValid)
                    {
                        var problemDetails = new HttpValidationProblemDetails(validationResult.ToDictionary())
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Title = "Validation failed",
                            Detail = "One or more validation errors occurred.",
                            Instance = "/api/auth/employee/login"
                        };
                        return Results.Problem(problemDetails);
                    }

                    var employee = await employeeRepo.GetByUsernameAsync(login.Username);

                    if (employee == null)
                        return Results.Unauthorized();

                    if (!BCrypt.Net.BCrypt.Verify(login.Password, employee.PasswordHash))
                        return Results.Unauthorized();

                    var token = tokenGenerator.GenerateToken(login.Username, employee.EmployeeId, Roles.Employee,
                        employee.Position);
                    var response = new LoginResponseDto(token, login.Username, employee.Position, employee.EmployeeId);

                    return Results.Ok(response);
                })
            .Accepts<LoginDto>("application/json")
            .Produces<LoginResponseDto>()
            .Produces(StatusCodes.Status401Unauthorized);
    }
}