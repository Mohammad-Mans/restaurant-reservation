using FluentValidation;
using RestaurantReservation.API.Dtos;

namespace RestaurantReservation.API.Validators;

internal sealed class PatchReservationDtoValidator : AbstractValidator<PatchReservationDto>
{
    public PatchReservationDtoValidator()
    {
        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("Customer ID must be greater than 0")
            .When(x => x.CustomerId.HasValue);

        RuleFor(x => x.RestaurantId)
            .GreaterThan(0).WithMessage("Restaurant ID must be greater than 0")
            .When(x => x.RestaurantId.HasValue);

        RuleFor(x => x.TableId)
            .GreaterThan(0).WithMessage("Table ID must be greater than 0")
            .When(x => x.TableId.HasValue);

        RuleFor(x => x.ReservationDate)
            .Must(date => date!.Value > DateTime.Now)
            .WithMessage("Reservation date must be in the future")
            .When(x => x.ReservationDate.HasValue);

        RuleFor(x => x.PartySize)
            .GreaterThan(0).WithMessage("Party size must be greater than 0")
            .LessThanOrEqualTo(20).WithMessage("Party size cannot exceed 20")
            .When(x => x.PartySize.HasValue);
    }
}