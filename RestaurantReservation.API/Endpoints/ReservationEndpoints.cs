using System.Security.Claims;
using FluentValidation;
using RestaurantReservation.API.Constants;
using RestaurantReservation.API.Dtos;
using RestaurantReservation.API.Mappings;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.Endpoints;

public static class ReservationEndpoints
{
    public static void MapReservationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/reservations").WithTags(ApiTags.Reservations);

        group.MapGet("/", async (
                IReservationRepository repo,
                int? pageNumber,
                int? pageSize,
                IValidator<PaginationQueryDto> validator) =>
            {
                var query = new PaginationQueryDto(pageNumber, pageSize);

                if (query.PageNumber == null && query.PageSize == null)
                {
                    var reservations = await repo.GetAllAsync();
                    return Results.Ok(reservations.Select(reservation => reservation.ToDto()).ToList());
                }

                var validationResult = await validator.ValidateAsync(query);
                if (!validationResult.IsValid)
                {
                    var problemDetails = new HttpValidationProblemDetails(validationResult.ToDictionary())
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Validation failed",
                        Detail = "One or more validation errors occurred.",
                        Instance = "/api/reservations"
                    };
                    return Results.Problem(problemDetails);
                }

                var pageNum = query.PageNumber ?? 1;
                var pageSz = query.PageSize ?? 10;

                var pagedResult = await repo.GetPagedAsync(pageNum, pageSz);
                var reservationsDtos = pagedResult.Items.Select(reservation => reservation.ToDto()).ToList();

                var response = new PaginatedResponseDto<ReservationResponseDto>(
                    reservationsDtos,
                    pagedResult.PageNumber,
                    pagedResult.PageSize,
                    pagedResult.TotalCount,
                    pagedResult.TotalPages
                );

                return Results.Ok(response);
            })
            .RequireAuthorization(policy => policy.RequireRole(Roles.Employee))
            .Produces<List<ReservationResponseDto>>()
            .Produces<PaginatedResponseDto<ReservationResponseDto>>()
            .WithSummary("Get list of reservations")
            .WithDescription(
                "Retrieves all reservations if no pagination parameters are provided, or a paginated list if pageNumber and/or pageSize are specified. Maximum page size is 100.");

        group.MapGet("/{id:int}", async (int id, IReservationRepository repo) =>
            {
                var reservation = await repo.GetByIdAsync(id);
                return reservation is not null ? Results.Ok(reservation.ToDto()) : Results.NotFound();
            })
            .RequireAuthorization(policy => policy.RequireRole(Roles.Employee))
            .Produces<ReservationResponseDto>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/", async (CreateReservationDto dto, IReservationRepository repo, ClaimsPrincipal user,
                IValidator<CreateReservationDto> validator) =>
            {
                var validationResult = await validator.ValidateAsync(dto);
                if (!validationResult.IsValid)
                {
                    var problemDetails = new HttpValidationProblemDetails(validationResult.ToDictionary())
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Validation failed",
                        Detail = "One or more validation errors occurred.",
                        Instance = "/api/reservations"
                    };
                    return Results.Problem(problemDetails);
                }

                if (user.IsInRole(Roles.Customer))
                {
                    var customerIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (customerIdClaim == null || !int.TryParse(customerIdClaim, out var customerId) ||
                        dto.CustomerId != customerId)
                        return Results.Forbid();
                }

                var reservation = new Reservation
                {
                    CustomerId = dto.CustomerId,
                    RestaurantId = dto.RestaurantId,
                    TableId = dto.TableId,
                    ReservationDate = dto.ReservationDate,
                    PartySize = dto.PartySize
                };

                var createdReservation = await repo.CreateAsync(reservation);
                return Results.Created($"/api/reservations/{createdReservation.ReservationId}",
                    createdReservation.ToDto());
            })
            .RequireAuthorization(policy => policy.RequireRole(Roles.Manager, Roles.Customer))
            .Accepts<CreateReservationDto>("application/json")
            .Produces<ReservationResponseDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        group.MapPut("/{id:int}", async (int id, UpdateReservationDto dto, IReservationRepository repo,
                ClaimsPrincipal user, IValidator<UpdateReservationDto> validator) =>
            {
                var validationResult = await validator.ValidateAsync(dto);
                if (!validationResult.IsValid)
                {
                    var problemDetails = new HttpValidationProblemDetails(validationResult.ToDictionary())
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Validation failed",
                        Detail = "One or more validation errors occurred.",
                        Instance = $"/api/reservations/{id}"
                    };
                    return Results.Problem(problemDetails);
                }

                var existingReservation = await repo.GetByIdAsync(id);
                if (existingReservation == null)
                    return Results.NotFound();

                if (user.IsInRole(Roles.Customer) && !user.IsInRole(Roles.Manager))
                {
                    var customerIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (customerIdClaim == null || !int.TryParse(customerIdClaim, out var customerId) ||
                        existingReservation.CustomerId != customerId)
                        return Results.Forbid();
                }

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
            .RequireAuthorization(policy => policy.RequireRole(Roles.Manager, Roles.Customer))
            .Accepts<UpdateReservationDto>("application/json")
            .Produces<ReservationResponseDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        group.MapPatch("/{id:int}",
                async (int id, PatchReservationDto reservationPatch, IReservationRepository repo, ClaimsPrincipal user,
                    IValidator<PatchReservationDto> validator) =>
                {
                    var validationResult = await validator.ValidateAsync(reservationPatch);
                    if (!validationResult.IsValid)
                    {
                        var problemDetails = new HttpValidationProblemDetails(validationResult.ToDictionary())
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Title = "Validation failed",
                            Detail = "One or more validation errors occurred.",
                            Instance = $"/api/reservations/{id}"
                        };
                        return Results.Problem(problemDetails);
                    }

                    var existingReservation = await repo.GetByIdAsync(id);
                    if (existingReservation is null)
                        return Results.NotFound();

                    if (user.IsInRole(Roles.Customer))
                    {
                        var customerIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                        if (customerIdClaim == null || !int.TryParse(customerIdClaim, out var customerId) ||
                            existingReservation.CustomerId != customerId)
                            return Results.Forbid();
                    }

                    if (reservationPatch.CustomerId.HasValue)
                        existingReservation.CustomerId = reservationPatch.CustomerId.Value;
                    if (reservationPatch.RestaurantId.HasValue)
                        existingReservation.RestaurantId = reservationPatch.RestaurantId.Value;
                    if (reservationPatch.TableId.HasValue)
                        existingReservation.TableId = reservationPatch.TableId.Value;
                    if (reservationPatch.ReservationDate.HasValue)
                        existingReservation.ReservationDate = reservationPatch.ReservationDate.Value;

                    if (reservationPatch.PartySize.HasValue)
                        existingReservation.PartySize = reservationPatch.PartySize.Value;

                    var updatedReservation = await repo.UpdateAsync(existingReservation);
                    return updatedReservation is not null ? Results.Ok(updatedReservation.ToDto()) : Results.NotFound();
                })
            .RequireAuthorization(policy => policy.RequireRole(Roles.Manager, Roles.Customer))
            .Accepts<PatchReservationDto>("application/json")
            .Produces<ReservationResponseDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        group.MapDelete("/{id:int}", async (int id, IReservationRepository repo, ClaimsPrincipal user) =>
            {
                var existingReservation = await repo.GetByIdAsync(id);
                if (existingReservation == null)
                    return Results.NotFound();

                if (user.IsInRole(Roles.Customer) && !user.IsInRole(Roles.Manager))
                {
                    var customerIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (customerIdClaim == null || !int.TryParse(customerIdClaim, out var customerId) ||
                        existingReservation.CustomerId != customerId)
                        return Results.Forbid();
                }

                var deletedReservation = await repo.DeleteAsync(id);
                return deletedReservation ? Results.NoContent() : Results.NotFound();
            })
            .RequireAuthorization(policy => policy.RequireRole(Roles.Manager, Roles.Customer))
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/customer/{customerId:int}", async (int customerId, IReservationRepository repo) =>
            {
                var reservations = await repo.GetByCustomerIdAsync(customerId);
                var reservationDtos = reservations.Select(reservation => reservation.ToDto()).ToList();
                return Results.Ok(reservationDtos);
            })
            .RequireAuthorization(policy => policy.RequireRole(Roles.Employee))
            .Produces<List<ReservationResponseDto>>();

        group.MapGet("/{reservationId:int}/orders", async (int reservationId, IOrderRepository repo) =>
            {
                var orders = await repo.GetByReservationIdAsync(reservationId);
                if (orders.Count == 0)
                    return Results.NotFound();

                var ordersDtos = orders.Select(order => order.ToDto()).ToList();
                return Results.Ok(ordersDtos);
            })
            .RequireAuthorization(policy => policy.RequireRole(Roles.Employee))
            .Produces<List<OrderResponseDto>>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/{reservationId:int}/menu-items", async (int reservationId, IMenuItemRepository repo) =>
            {
                var menuItems = await repo.GetByReservationIdAsync(reservationId);
                if (menuItems.Count == 0)
                    return Results.NotFound();

                var menuItemsDtos = menuItems.Select(menuItem => menuItem.ToDto()).ToList();
                return Results.Ok(menuItemsDtos);
            })
            .RequireAuthorization(policy => policy.RequireRole(Roles.Employee))
            .Produces<List<MenuItemResponseDto>>()
            .Produces(StatusCodes.Status404NotFound);
    }
}