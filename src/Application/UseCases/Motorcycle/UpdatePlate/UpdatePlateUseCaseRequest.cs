using Shared.Abstracts;

namespace Application.UseCases.Motorcycle.UpdatePlate;

public class UpdatePlateUseCaseRequest(Guid id, string plate) : LogRequest
{
    public Guid Id { get; } = id;
    public string Plate { get; } = plate;
}