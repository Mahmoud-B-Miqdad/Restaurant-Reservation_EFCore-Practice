using FluentValidation;
using RestaurantReservationSystem.API.DTOs;

namespace RestaurantReservationSystem.API.Validators
{
    public class RestaurantRequestValidator : AbstractValidator<RestaurantRequest>
    {
        public RestaurantRequestValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("Restaurant name is required.")
                .MaximumLength(100).WithMessage("Restaurant name must not exceed 100 characters.");

            RuleFor(r => r.Address)
                .MaximumLength(200).WithMessage("Address must not exceed 200 characters.");

            RuleFor(r => r.PhoneNumber)
                .Matches(@"^\d{10,15}$")
                .When(r => !string.IsNullOrWhiteSpace(r.PhoneNumber))
                .WithMessage("Phone number must be between 10 and 15 digits.");

            RuleFor(r => r.OpeningHours)
                .NotEmpty().WithMessage("Opening hours are required.")
                .MaximumLength(100).WithMessage("Opening hours must not exceed 100 characters.");
        }
    }
}