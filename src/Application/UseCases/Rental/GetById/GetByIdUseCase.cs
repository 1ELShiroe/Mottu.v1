using Shared.Abstracts;
using Shared.Dtos;
using Shared.Interfaces.Repositories;
using Shared.Mappers;

namespace Application.UseCases.Rental.GetById;

public class GetByIdUseCase(
    IRentalSubscriptionRepository _rentalSubscriptionRepository,
    ILogRepository _logRepository) : UseCase<GetByIdUseCaseRequest, RentalSubscriptionDto>
{
    public override UseCaseResult<RentalSubscriptionDto> Execute(GetByIdUseCaseRequest req)
    {
        try
        {
            req.AddInfo($"Consultando locação com Id={req.Id}");

            var existSubscription = _rentalSubscriptionRepository.GetById(req.Id);

            if (existSubscription == null)
            {
                req.AddWarning($"Locação {req.Id} não encontrada.");
                return Result.NotFound("Locação não encontrada");
            }

            req.AddInfo($"Locação {req.Id} encontrada com sucesso.");
            return Result.Ok("Locação encontrada com sucesso", existSubscription.ToDto());
        }
        catch (Exception ex)
        {
            req.AddError($"Erro ao consultar locação {req.Id}: {ex.Message}");
            return Result.InternalError($"Erro ao consultar a locação: {ex.Message}");
        }
        finally
        {
            // TODO: Implement logging here (e.g., success, failure, exceptions)
            _logRepository.AddRange([.. req.Logs]);
        }
    }
}