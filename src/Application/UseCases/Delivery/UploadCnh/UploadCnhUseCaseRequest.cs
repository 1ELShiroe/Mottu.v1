using Shared.Abstracts;

namespace Application.UseCases.Delivery.UploadCnh;

public class UploadCnhUseCaseRequest(Guid id, string base64cnh): LogRequest
{
    public Guid Id { get; } = id;
    public string Base64Cnh { get; } = base64cnh;
}