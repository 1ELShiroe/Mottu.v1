using Shared.Abstracts;
using Shared.Interfaces.Repositories;
using Shared.Interfaces.Services;
using Shared.Libs;

namespace Application.UseCases.Delivery.UploadCnh;

public class UploadCnhUseCase(
    IDeliveryRepository _deliveryRepository,
    IStorageService _storageService,
    ILogRepository _logRepository) : UseCase<UploadCnhUseCaseRequest, string>
{
    public override UseCaseResult<string> Execute(UploadCnhUseCaseRequest req)
    {
        try
        {
            req.AddInfo($"Iniciando upload de CNH para entregador {req.Id}.");

            var existDelivery = _deliveryRepository.GetById(req.Id);
            var isValidImage = IsBase64PngOrBmp.Valid(req.Base64Cnh);

            if (existDelivery == null || !isValidImage)
            {
                req.AddWarning("Entrega não encontrada ou imagem inválida.");
                return Result.Fail("Dados inválidos");
            }

            byte[] imageBytes = Convert.FromBase64String(req.Base64Cnh);
            using var stream = new MemoryStream(imageBytes);

            string fileExtension = imageBytes[0] == 0x42 && imageBytes[1] == 0x4D ? "bmp" : "png";
            string fileName = $"cnh_{req.Id}_{DateTime.UtcNow:yyyyMMddHHmmss}.{fileExtension}";
            string contentType = fileExtension == "png" ? "image/png" : "image/bmp";

            var url = _storageService.UploadAsync(stream, fileName, contentType).GetAwaiter().GetResult();

            existDelivery.SetCnhImage(url);
            _deliveryRepository.Update(existDelivery);

            req.AddInfo("Foto da CNH enviada com sucesso.");
            return Result.Created("Foto recebida com sucesso");
        }
        catch (Exception ex)
        {
            req.AddError($"Erro ao registrar a foto da CNH: {ex.Message}");
            return Result.InternalError($"Erro ao registrar a foto: {ex.Message}");
        }
        finally
        {
            // TODO: Implement logging here (e.g., success, failure, exceptions)
            _logRepository.AddRange([.. req.Logs]);
        }
    }
}
