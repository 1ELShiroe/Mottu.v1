using Shared.Abstracts;
using Shared.Interfaces.Repositories;

namespace Application.UseCases.Delivery.Create;

public class CreateUseCase(
    IDeliveryRepository _deliveryRepository,
    ILogRepository _logRepository) : UseCase<CreateUseCaseRequest, string>
{
    public override UseCaseResult<string> Execute(CreateUseCaseRequest req)
    {
        try
        {
            req.AddInfo("Iniciando criação de entregador.");

            var model = Shared.Models.Delivery.New(
                req.Name, req.Cnpj, req.BirthDate, req.CnhNumber, req.CnhType, "");

            if (!model.IsValid)
            {
                req.AddWarning("Dados do entregador inválidos.");
                return Result.Fail("Dados inválidos");
            }

            var isRepeatPlate = _deliveryRepository.GetExpress(d => d.Cnpj == req.Cnpj || d.CnhNumber == req.CnhNumber);

            if (isRepeatPlate != null)
            {
                req.AddWarning("Entregador com CNPJ ou CNH já existente.");
                return Result.NotFound("Dados inválidos");
            }

            _deliveryRepository.Add(model);

            req.AddInfo("Entregador criado com sucesso.");
            return Result.Created("Entregador criado com sucesso");
        }
        catch (Exception ex)
        {
            req.AddError($"Erro ao criar entregador: {ex.Message}");
            return Result.InternalError($"Erro ao registrar entregador: {ex.Message}");
        }
        finally
        {
            // TODO: Implement logging here (e.g., success, failure, exceptions)
            _logRepository.AddRange([.. req.Logs]);
        }
    }
}