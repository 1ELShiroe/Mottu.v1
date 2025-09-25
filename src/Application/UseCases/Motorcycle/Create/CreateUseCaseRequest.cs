using Shared.Abstracts;

namespace Application.UseCases.Motorcycle.Create;

public class CreateUseCaseRequest(int year, string model, string plate) : LogRequest
{
    public int Year { get; } = year;
    public string Model { get; } = model;
    public string Plate { get; } = plate;
}