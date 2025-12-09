using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantReservation.API.Auth;
using RestaurantReservation.API.Constants;
using RestaurantReservation.API.Dtos;
using RestaurantReservation.API.Mappings;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RestaurantReservationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();

builder.Services.AddSingleton<JwtTokenGenerator>();

var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSettings["Key"];

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),

            ValidateIssuer = true,
            ValidIssuer = jwtSettings["Issuer"],

            ValidateAudience = true,
            ValidAudience = jwtSettings["Audience"],

            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/api/reservations", async (IReservationRepository repo) =>
    {
        var reservations = await repo.GetAllAsync();
        var reservationsDtos = reservations.Select(reservation => reservation.ToDto()).ToList();
        return Results.Ok(reservationsDtos);
    })
    .Produces<List<ReservationResponseDto>>()
    .WithTags(ApiTags.Reservations);

app.MapGet("/api/reservations/{id:int}", async (int id, IReservationRepository repo) =>
    {
        var reservation = await repo.GetByIdAsync(id);
        return reservation is not null ? Results.Ok(reservation.ToDto()) : Results.NotFound();
    })
    .Produces<ReservationResponseDto>()
    .Produces(StatusCodes.Status404NotFound)
    .WithTags(ApiTags.Reservations);

app.MapPost("/api/reservations", async (CreateReservationDto dto, IReservationRepository repo) =>
    {
        if (dto.ReservationDate < DateTime.Now)
            return Results.BadRequest("Reservation date must be in the future");

        var reservation = new Reservation
        {
            CustomerId = dto.CustomerId,
            RestaurantId = dto.RestaurantId,
            TableId = dto.TableId,
            ReservationDate = dto.ReservationDate,
            PartySize = dto.PartySize
        };

        var createdReservation = await repo.CreateAsync(reservation);
        return Results.Created($"/api/reservations/{createdReservation.ReservationId}", createdReservation.ToDto());
    })
    .Accepts<CreateReservationDto>("application/json")
    .Produces<ReservationResponseDto>(StatusCodes.Status201Created)
    .Produces(StatusCodes.Status400BadRequest)
    .WithTags(ApiTags.Reservations);

app.MapPut("/api/reservations/{id:int}", async (int id, UpdateReservationDto dto, IReservationRepository repo) =>
    {
        if (dto.ReservationDate < DateTime.Now)
            return Results.BadRequest("Reservation date must be in the future");

        var reservation = new Reservation
        {
            ReservationId = id,
            CustomerId = dto.CustomerId,
            RestaurantId = dto.RestaurantId,
            TableId = dto.TableId,
            ReservationDate = dto.ReservationDate,
            PartySize = dto.PartySize
        };

        var updatedReservation = await repo.UpdateAsync(reservation);
        return updatedReservation is not null ? Results.Ok(updatedReservation.ToDto()) : Results.NotFound();
    })
    .Accepts<UpdateReservationDto>("application/json")
    .Produces<ReservationResponseDto>()
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status404NotFound)
    .WithTags(ApiTags.Reservations);

app.MapPatch("/api/reservations/{id:int}", async (int id, PatchReservationDto reservationPatch, IReservationRepository repo) =>
    {
        var existingReservation = await repo.GetByIdAsync(id);
        if (existingReservation is null)
            return Results.NotFound();

        if (reservationPatch.CustomerId.HasValue)
            existingReservation.CustomerId = reservationPatch.CustomerId.Value;
        if (reservationPatch.RestaurantId.HasValue)
            existingReservation.RestaurantId = reservationPatch.RestaurantId.Value;
        if (reservationPatch.TableId.HasValue)
            existingReservation.TableId = reservationPatch.TableId.Value;
        if (reservationPatch.ReservationDate.HasValue)
        {
            if (reservationPatch.ReservationDate.Value < DateTime.Now)
                return Results.BadRequest("Reservation date must be in the future");
            existingReservation.ReservationDate = reservationPatch.ReservationDate.Value;
        }
        if (reservationPatch.PartySize.HasValue)
            existingReservation.PartySize = reservationPatch.PartySize.Value;

        var updatedReservation = await repo.UpdateAsync(existingReservation);
        return updatedReservation is not null ? Results.Ok(updatedReservation.ToDto()) : Results.NotFound();
    })
    .Accepts<PatchReservationDto>("application/json")
    .Produces<ReservationResponseDto>()
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status404NotFound)
    .WithTags(ApiTags.Reservations);

app.MapDelete("/api/reservations/{id:int}", async (int id, IReservationRepository repo) =>
    {
        var deletedReservation = await repo.DeleteAsync(id);
        return deletedReservation ? Results.NoContent() : Results.NotFound();
    })
    .Produces(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status404NotFound)
    .WithTags(ApiTags.Reservations);

app.MapGet("/api/employees/managers", async (IEmployeeRepository repo) =>
    {
        var managers = await repo.ListManagersAsync();
        var managersDtos = managers.Select(manager => manager.ToDto()).ToList();
        return Results.Ok(managersDtos);
    })
    .Produces<List<EmployeeResponseDto>>()
    .WithTags(ApiTags.Employees);

app.MapGet("/api/reservations/customer/{customerId:int}", async (int customerId, IReservationRepository repo) =>
    {
        var reservations = await repo.GetByCustomerIdAsync(customerId);
        var reservationDtos = reservations.Select(reservation => reservation.ToDto()).ToList();
        return Results.Ok(reservationDtos);
    })
    .Produces<List<ReservationResponseDto>>()
    .WithTags(ApiTags.Reservations);

app.MapGet("/api/reservations/{reservationId:int}/orders", async (int reservationId, IOrderRepository repo) =>
    {
        var orders = await repo.GetByReservationIdAsync(reservationId);
        if (orders.Count == 0)
            return Results.NotFound();

        var ordersDtos = orders.Select(order => order.ToDto()).ToList();
        return Results.Ok(ordersDtos);
    })
    .Produces<List<OrderResponseDto>>()
    .Produces(StatusCodes.Status404NotFound)
    .WithTags(ApiTags.Reservations);

app.MapGet("/api/reservations/{reservationId:int}/menu-items", async (int reservationId, IMenuItemRepository repo) =>
    {
        var menuItems = await repo.GetByReservationIdAsync(reservationId);
        if (menuItems.Count == 0)
            return Results.NotFound();

        var menuItemsDtos = menuItems.Select(menuItem => menuItem.ToDto()).ToList();
        return Results.Ok(menuItemsDtos);
    })
    .Produces<List<MenuItemResponseDto>>()
    .Produces(StatusCodes.Status404NotFound)
    .WithTags(ApiTags.Reservations);

app.MapGet("/api/employees/{employeeId:int}/average-order-amount", async (int employeeId, IOrderRepository repo) =>
    {
        var averageAmount = await repo.GetAverageOrderAmountByEmployeeIdAsync(employeeId);
        var response = new AverageOrderAmountResponseDto(employeeId, averageAmount);
        return averageAmount.HasValue
            ? Results.Ok(response)
            : Results.NotFound(response);
    })
    .Produces<AverageOrderAmountResponseDto>()
    .Produces<AverageOrderAmountResponseDto>(StatusCodes.Status404NotFound)
    .WithTags(ApiTags.Employees);

app.MapPost("/api/auth/customer/login",
        async (LoginDto login, ICustomerRepository customerRepo, JwtTokenGenerator tokenGenerator) =>
        {
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
    .Produces(StatusCodes.Status401Unauthorized)
    .WithTags(ApiTags.Authentication);

app.MapPost("/api/auth/employee/login",
        async (LoginDto login, IEmployeeRepository employeeRepo, JwtTokenGenerator tokenGenerator) =>
        {
            var employee = await employeeRepo.GetByUsernameAsync(login.Username);

            if (employee == null)
                return Results.Unauthorized();

            if (!BCrypt.Net.BCrypt.Verify(login.Password, employee.PasswordHash))
                return Results.Unauthorized();

            var token = tokenGenerator.GenerateToken(login.Username, employee.EmployeeId, Roles.Employee, employee.Position);
            var response = new LoginResponseDto(token, login.Username, employee.Position, employee.EmployeeId);

            return Results.Ok(response);
        })
    .Accepts<LoginDto>("application/json")
    .Produces<LoginResponseDto>()
    .Produces(StatusCodes.Status401Unauthorized)
    .WithTags(ApiTags.Authentication);

app.Run();