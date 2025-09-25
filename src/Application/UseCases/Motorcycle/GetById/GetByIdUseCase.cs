using Shared.Abstracts;
using Shared.Dtos;
using Shared.Interfaces.Repositories;
using Shared.Mappers;

namespace Application.UseCases.Motorcycle.GetById;

public class GetByIdUseCase(
    IMotorcycleRepository _motorcycleRepository,
    ILogRepository _logRepository) : UseCase<GetByIdUseCaseRequest, MotorcycleDto>
{
    public override UseCaseResult<MotorcycleDto> Execute(GetByIdUseCaseRequest req)
    {
        try
        {
            var data = _motorcycleRepository.GetById(req.Id);

            if (data == null)
            {
                req.AddWarning($"Moto {req.Id} não encontrada.");
                return Result.NotFound("Moto não encontrada");
            }

            req.AddInfo($"Moto {req.Id} encontrada com sucesso.");
            return Result.Ok("Moto encontrada com sucesso", data.ToDto());
        }
        catch (Exception ex)
        {
            req.AddError($"Erro ao buscar moto {req.Id}: {ex.Message}");
            return Result.InternalError("Dados inválidos");
        }
        finally
        {
            // TODO: Implement logging here (e.g., success, failure, exceptions)
            _logRepository.AddRange([.. req.Logs]);
        }
    }
}