using FluentValidation;
using FluentValidation.Results;

namespace Shared.Abstracts;

public abstract class Entity
{
    public Guid Id { get; protected set; } = Guid.CreateVersion7();
    public bool IsValid { get; private set; }
    public ValidationResult? ValidationResult { get; private set; }

    public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
    {
        ValidationResult = validator.Validate(model);

        return IsValid = ValidationResult.IsValid;
    }

    public void SetId(Guid id) => Id = id;
}
