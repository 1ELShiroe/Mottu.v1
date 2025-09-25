using Shared.Abstracts;

namespace Application.UseCases.Rental.GetById;

public class GetByIdUseCaseRequest(Guid id) : LogRequest
{
    public Guid Id { get; } = id;
}