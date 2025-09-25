using Shared.Abstracts;

namespace Application.UseCases.Motorcycle.GetById;

public class GetByIdUseCaseRequest(Guid id) : LogRequest
{
    public Guid Id { get; } = id;
}