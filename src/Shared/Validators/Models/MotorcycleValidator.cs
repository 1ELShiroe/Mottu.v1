using FluentValidation;
using Shared.Models;

namespace Shared.Validators.Models;

public class MotorcycleValidator : AbstractValidator<Motorcycle>
{
    public MotorcycleValidator()
    {
        RuleFor(m => m.Year)
            .NotEmpty().WithMessage("Year cannot be empty.")
            .InclusiveBetween(1900, DateTime.Now.Year).WithMessage($"Year must be between 1900 and {DateTime.Now.Year}.");

        RuleFor(m => m.Model)
            .NotEmpty().WithMessage("Model cannot be empty.")
            .MaximumLength(100).WithMessage("Model cannot exceed 100 characters.");

        RuleFor(m => m.Plate)
                .NotEmpty().WithMessage("Plate cannot be empty.")
                .Matches(@"^[A-Z]{3}[0-9][A-Z0-9][0-9]{2}$")
                .WithMessage("The provided Plate format is invalid.");
    }
}