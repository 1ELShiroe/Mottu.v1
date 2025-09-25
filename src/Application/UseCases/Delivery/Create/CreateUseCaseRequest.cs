using Shared.Abstracts;

namespace Application.UseCases.Delivery.Create;

public class CreateUseCaseRequest(
    string name,
    string cnpj,
    DateTime birthDate,
    string cnhNumber,
    string cnhType) : LogRequest
{
    public string Name { get; } = name;
    public string Cnpj { get; } = cnpj;
    public DateTime BirthDate { get; } = birthDate;
    public string CnhNumber { get; } = cnhNumber;
    public string CnhType { get; } = cnhType;
}