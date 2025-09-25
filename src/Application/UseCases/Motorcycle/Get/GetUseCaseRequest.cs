using Shared.Abstracts;

namespace Application.UseCases.Motorcycle.Get;

public class GetUseCaseRequest(string? year, string? model, string? plate) : LogRequest
{
    public string? Year { get; } = year;
    public string? Model { get; } = model;
    public string? Plate { get; } = plate;
}