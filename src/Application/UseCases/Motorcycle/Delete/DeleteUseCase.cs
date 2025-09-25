using Shared.Abstracts;
using Shared.Interfaces.Repositories;

namespace Application.UseCases.Motorcycle.Delete;

public class DeleteUseCase(
    IMotorcycleRepository _motorcycleRepository,
    ILogRepository _logRepository) : UseCase<DeleteUseCaseRequest, string>
{
    public override UseCaseResult<string> Execute(DeleteUseCaseRequest req)
    {
        try
        {
            var data = _motorcycleRepository.GetById(req.Id);

            if (data == null)
            {
                req.AddWarning($"Tentativa de deletar moto não encontrada: Id={req.Id}");
                return Result.NotFound("Dados inválidos");
            }

            _motorcycleRepository.DeleteById(req.Id);

            req.AddInfo($"Moto deletada com sucesso: Id={req.Id}");
            return Result.Ok("Moto removida com sucesso");
        }
        catch (Exception ex)
        {
            req.AddError($"Erro ao deletar moto {req.Id}: {ex.Message}");
            return Result.InternalError("Dados inválidos");
        }
        finally
        {
            // TODO: Implement logging here (e.g., success, failure, exceptions)
            _logRepository.AddRange([.. req.Logs]);
        }
    }
}