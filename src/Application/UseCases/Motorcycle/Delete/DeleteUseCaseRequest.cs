using Shared.Abstracts;

namespace Application.UseCases.Motorcycle.Delete;

public class DeleteUseCaseRequest(Guid id) : LogRequest
{
    public Guid Id { get; } = id;
}