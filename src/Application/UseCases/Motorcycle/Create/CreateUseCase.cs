using Shared.Abstracts;
using Shared.Dtos;
using Shared.Interfaces;
using Shared.Interfaces.Repositories;
using Shared.Mappers;

namespace Application.UseCases.Motorcycle.Create;

public class CreateUseCase(
    IMotorcycleRepository _motorcycleRepository,
    IPublisher<MotorcycleDto> _publisher,
    ILogRepository _logRepository) : UseCase<CreateUseCaseRequest, string>
{
    public override UseCaseResult<string> Execute(CreateUseCaseRequest req)
    {
        try
        {
            var model = Shared.Models.Motorcycle.New(req.Year, req.Model, req.Plate);

            if (!model.IsValid)
            {
                req.AddWarning($"Tentativa de criar moto com dados inválidos: Ano={req.Year}, Modelo={req.Model}, Placa={req.Plate}");
                return Result.Fail("Dados inválidos");
            }

            var isRepeatPlate = _motorcycleRepository.GetExpress(m => m.Plate == req.Plate);

            if (isRepeatPlate != null)
            {
                req.AddWarning($"Tentativa de criar moto com placa repetida: {req.Plate}");
                return Result.NotFound("Dados inválidos");
            }

            _motorcycleRepository.Add(model);

            _publisher.PublishAsync(model.ToDto());

            req.AddInfo($"Moto registrada com sucesso: {req.Plate}, Modelo={req.Model}, Ano={req.Year}");
            return Result.Created("Moto registrada com sucesso");
        }
        catch (Exception ex)
        {
            req.AddError($"Erro ao registrar moto: {ex.Message}");
            return Result.InternalError("Dados inválidos");
        }
        finally
        {
            // TODO: Implement logging here (e.g., success, failure, exceptions)
            _logRepository.AddRange([.. req.Logs]);
        }
    }
}