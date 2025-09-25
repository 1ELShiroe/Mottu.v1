using Shared.Abstracts;
using Shared.Dtos;
using Shared.Interfaces;
using Shared.Interfaces.Repositories;
using Shared.Mappers;
using Shared.Models;

namespace Application.UseCases.Rental.CloseRentalUseCase;

public class CloseRentalUseCase(
    IRentalSubscriptionRepository _rentalSubscriptionRepository,
    IPublisher<PaymentDto> _paymentPublisher,
    ILogRepository _logRepository) : UseCase<CloseRentalUseCaseRequest, string>
{
    public override UseCaseResult<string> Execute(CloseRentalUseCaseRequest req)
    {
        try
        {
            req.AddInfo($"Iniciando fechamento da locação Id={req.Id} com data de devolução {req.ReturnDate}");

            var existSubActive = _rentalSubscriptionRepository.GetExpress(r => r.Id == req.Id && !r.ReturnDate.HasValue);

            if (existSubActive == null)
            {
                req.AddWarning($"Locação não encontrada ou já finalizada: Id={req.Id}");
                return Result.Fail("Dados inválidos");
            }

            existSubActive.SetReturnDate(req.ReturnDate);

            _rentalSubscriptionRepository.Update(existSubActive);

            var payment = Payment.New(existSubActive);

            _paymentPublisher.PublishAsync(payment.ToDto());
            req.AddInfo($"Locação finalizada com sucesso: Id={req.Id}, Total a pagar={payment.TotalAmount}");

            return Result.Ok("Data de devolução informada com sucesso");
        }
        catch (Exception ex)
        {
            req.AddError($"Erro ao fechar locação Id={req.Id}: {ex.Message}");
            return Result.InternalError("Dados inválidos");
        }
        finally
        {
            // TODO: Implement logging here (e.g., success, failure, exceptions)
            _logRepository.AddRange([.. req.Logs]);
        }
    }
}