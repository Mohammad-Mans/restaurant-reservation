using FluentValidation;
using RestaurantReservation.API.Dtos;

namespace RestaurantReservation.API.Validators;

internal sealed class CreateReservationDtoValidator : AbstractValidator<CreateReservationDto>
{
    public CreateReservationDtoValidator()
    {
        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("Customer ID must be greater than 0");

        RuleFor(x => x.RestaurantId)
            .GreaterThan(0).WithMessage("Restaurant ID must be greater than 0");

        RuleFor(x => x.TableId)
            .GreaterThan(0).WithMessage("Table ID must be greater than 0");

        RuleFor(x => x.ReservationDate)
            .NotEmpty().WithMessage("Reservation date is required")
            .Must(date => date > DateTime.Now)
            .WithMessage("Reservation date must be in the future");

        RuleFor(x => x.PartySize)
            .GreaterThan(0).WithMessage("Party size must be greater than 0")
            .LessThanOrEqualTo(20).WithMessage("Party size cannot exceed 20");
    }
}