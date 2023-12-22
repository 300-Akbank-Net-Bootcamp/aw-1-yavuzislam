using FluentValidation;
using VbApi.Controllers;

namespace VbApi.Validations;

public class EmployeeValidator : AbstractValidator<Employee>
{
    public EmployeeValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Name is required")
            .MinimumLength(10)
            .MaximumLength(250)
            .WithMessage("Invalid Name");

        RuleFor(x => x.DateOfBirth)
            .NotNull()
            .WithMessage("Birth date is required")
            .Must(BeValidBirthDate)
            .WithMessage("Invalid birth date");

        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email))
            .WithMessage("Email address is not valid.");

        RuleFor(x => x.Phone)
            .NotNull()
            .NotEmpty().Matches(@"^(?:\(\d{3}\)\s?|\d{3}\s?)\d{3}[\s-]?\d{2}[\s-]?\d{2}$")
            .WithMessage("Phone is not valid.");

        RuleFor(x => x.HourlySalary)
            .NotNull()
            .NotEmpty()
            .WithMessage("Hourly salary is required")
            .InclusiveBetween(50, 400)
            .Must(BeValidHourlySalary)
            .WithMessage("Minimum hourly salary is not valid");
    }

    private bool BeValidHourlySalary(Employee employee, double hourlySalary)
    {
        var dateBeforeThirtyYears = DateTime.Today.AddYears(-30);
        var isOlderThanThirdyYears = employee.DateOfBirth <= dateBeforeThirtyYears;
        var isValidSalary = isOlderThanThirdyYears ? hourlySalary >= 200 : hourlySalary >= 50;
        return isValidSalary;
    }

    private bool BeValidBirthDate(DateTime dateOfBirth)
    {
        var minAllowedBirthDate = DateTime.Today.AddYears(-65);
        return minAllowedBirthDate <= dateOfBirth;
    }
}