using Shared.Abstracts;

namespace Application.UseCases.Rental.CloseRentalUseCase;

public class CloseRentalUseCaseRequest(Guid id, DateTime returnDate) : LogRequest
{
    public Guid Id { get; } = id;
    public DateTime ReturnDate { get; } = returnDate;
}
