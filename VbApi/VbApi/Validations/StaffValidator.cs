using FluentValidation;
using VbApi.Controllers;

namespace VbApi.Validations;

public class StaffValidator : AbstractValidator<Staff>
{
    public StaffValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(10).WithMessage("Name length must be at least 10 characters.")
            .MaximumLength(250).WithMessage("Name length must be at most 250 characters.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email))
            .WithMessage("Email address is not valid.");

        RuleFor(x => x.Phone)
            .NotEmpty().Matches(@"^(?:\(\d{3}\)\s?|\d{3}\s?)\d{3}[\s-]?\d{2}[\s-]?\d{2}$")
            .WithMessage("Phone is not valid.");


        RuleFor(x => x.HourlySalary)
            .NotEmpty()
            .WithMessage("Hourly salary is required.")
            .InclusiveBetween(30, 400)
            .WithMessage("Hourly salary must fall within the range of 30 to 400.");
    }
}