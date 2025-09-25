using Shared.Abstracts;
using Shared.Interfaces.Repositories;
using Shared.Models;

namespace Application.UseCases.Rental.Create;

public class CreateUseCase(
    IDeliveryRepository _deliveryRepository,
    IMotorcycleRepository _motorcycleRepository,
    IRentalPlanRepository _rentalPlanRepository,
    IRentalSubscriptionRepository _rentalSubscriptionRepository,
    ILogRepository _logRepository) : UseCase<CreateUseCaseRequest, string>
{
    public override UseCaseResult<string> Execute(CreateUseCaseRequest req)
    {
        try
        {
            req.AddInfo($"Iniciando criação de locação para DeliveryId={req.DeliveryId}, MotorcycleId={req.MotorcycleId}, Plan={req.Plan}");

            var existDelivery = _deliveryRepository.GetExpress(d => d.Id == req.DeliveryId && d.CnhType == "A");
            var existMotorcycle = _motorcycleRepository.GetById(req.MotorcycleId);
            var existPlan = _rentalPlanRepository.GetExpress(r => r.DurationInDays == req.Plan);

            if (existDelivery == null || existMotorcycle == null || existPlan == null)
            {
                req.AddWarning("Entrega, moto ou plano não encontrados ou inválidos");
                return Result.NotFound("Dados inválidos");
            }

            var newSubscription = RentalSubscription.New(
                req.DeliveryId, req.MotorcycleId, existPlan.Id, existPlan, DateTime.Now);

            _rentalSubscriptionRepository.Add(newSubscription);

            req.AddInfo($"Locação criada com sucesso: SubscriptionId={newSubscription.Id}");

            return Result.Created("Solicitação de locação recebida com sucesso");
        }
        catch (Exception ex)
        {
            req.AddError($"Erro ao criar locação: {ex.Message}");
            return Result.InternalError("Dados inválidos");
        }
        finally
        {
            // TODO: Implement logging here (e.g., success, failure, exceptions)
            _logRepository.AddRange([.. req.Logs]);
        }
    }
}