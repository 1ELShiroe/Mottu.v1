using Shared.Abstracts;
using Shared.Dtos;
using Shared.Interfaces.Repositories;
using Shared.Mappers;

namespace Application.UseCases.Motorcycle.UpdatePlate;

public class UpdatePlateUseCase(
    IMotorcycleRepository _motorcycleRepository,
    ILogRepository _logRepository) : UseCase<UpdatePlateUseCaseRequest, MotorcycleDto>
{
    public override UseCaseResult<MotorcycleDto> Execute(UpdatePlateUseCaseRequest req)
    {
        try
        {
            var motorcycle = _motorcycleRepository.GetById(req.Id);

            if (motorcycle == null)
            {
                req.AddWarning($"Moto {req.Id} não encontrada para atualização de placa.");
                return Result.Fail("Dados inválidos");
            }

            var newPlate = Shared.Models.Motorcycle.New(motorcycle.Year, motorcycle.Model, req.Plate);

            if (!newPlate.IsValid)
            {
                req.AddWarning($"Nova placa '{req.Plate}' é inválida para a moto {req.Id}.");
                return Result.Fail("Dados inválidos");
            }

            _motorcycleRepository.Update(newPlate);

            req.AddInfo($"Placa da moto {req.Id} atualizada com sucesso para '{req.Plate}'.");
            return Result.Ok("Placa modificada com sucesso", motorcycle.ToDto());
        }
        catch (Exception ex)
        {
            req.AddError($"Erro ao atualizar placa da moto {req.Id}: {ex.Message}");
            return Result.InternalError("Dados inválidos");
        }
        finally
        {
            // TODO: Implement logging here (e.g., success, failure, exceptions)
            _logRepository.AddRange([.. req.Logs]);
        }
    }
}
