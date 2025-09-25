using Shared.Abstracts;
using Shared.Dtos;
using Shared.Interfaces.Repositories;
using Shared.Mappers;

namespace Application.UseCases.Motorcycle.Get;

public class GetUseCase(
    IMotorcycleRepository _motorcycleRepository,
    ILogRepository _logRepository) : UseCase<GetUseCaseRequest, MotorcycleDto[]>
{
    public override UseCaseResult<MotorcycleDto[]> Execute(GetUseCaseRequest req)
    {
        try
        {
            var data = _motorcycleRepository.GetRangeExpress(m =>
                (string.IsNullOrWhiteSpace(req.Plate) || m.Plate.Contains(req.Plate)) &&
                (string.IsNullOrWhiteSpace(req.Model) || m.Model.Contains(req.Model)) &&
                (string.IsNullOrWhiteSpace(req.Year) || m.Year == int.Parse(req.Year))
            );

            req.AddInfo($"{data.Length} moto(s) encontrada(s) com sucesso.");
            return Result.Ok("Motos encontradas com sucesso", data.ToDto());
        }
        catch (Exception ex)
        {
            req.AddError($"Erro ao buscar motos: {ex.Message}");
            return Result.InternalError("Dados inválidos");
        }
        finally
        {
            // TODO: Implement logging here (e.g., success, failure, exceptions)
            _logRepository.AddRange([.. req.Logs]);
        }
    }
}