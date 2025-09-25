using FluentValidation;
using Shared.Models;

namespace Shared.Validators.Models;

public class DeliveryValidator : AbstractValidator<Delivery>
{
    public DeliveryValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MaximumLength(200).WithMessage("Name cannot exceed 200 characters.");

        RuleFor(d => d.Cnpj)
            .NotEmpty().WithMessage("CNPJ cannot be empty.")
            .Length(14).WithMessage("CNPJ must have 14 digits.");

        RuleFor(d => d.BirthDate)
            .NotEmpty().WithMessage("Birth date cannot be empty.")
            .LessThan(DateTime.Now).WithMessage("Birth date must be in the past.");

        RuleFor(d => d.CnhNumber)
            .NotEmpty().WithMessage("CNH number cannot be empty.")
            .Length(11).WithMessage("CNH number must have 11 digits.");

        RuleFor(d => d.CnhType)
            .NotEmpty().WithMessage("CNH type cannot be empty.")
            .Must(type => new[] { "A", "B", "A+B" }.Contains(type))
            .WithMessage("CNH type must be one of: A, B, C, D, E.");

        RuleFor(d => d.CnhImage)
            .MaximumLength(10_000).WithMessage("CNH image is too large.");
    }
}
