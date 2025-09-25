using Shared.Abstracts;

namespace Application.UseCases.Rental.Create;

public class CreateUseCaseRequest(
    Guid deliveryId,
    Guid motorcycleId,
    DateTime startDate,
    DateTime endDate,
    DateTime expectedEndDate,
    int plan) : LogRequest
{
    public Guid DeliveryId { get; } = deliveryId;
    public Guid MotorcycleId { get; } = motorcycleId;
    public DateTime StartDate { get; } = startDate;
    public DateTime EndDate { get; } = endDate;
    public DateTime ExpectedEndDate { get; } = expectedEndDate;
    public int Plan { get; } = plan;
}
