using FluentValidation;
using RestaurantReservation.API.Dtos;

namespace RestaurantReservation.API.Validators;

public class PaginationQueryDtoValidator : AbstractValidator<PaginationQueryDto>
{
    public PaginationQueryDtoValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .When(x => x.PageNumber.HasValue)
            .WithMessage("Page number must be greater than 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(100)
            .When(x => x.PageSize.HasValue)
            .WithMessage("Page size must be between 1 and 100.");
    }
}