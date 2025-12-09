using Microsoft.EntityFrameworkCore;
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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


app.Run();