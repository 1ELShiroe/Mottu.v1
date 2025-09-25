using Api.Extensions;
using Application.UseCases.Delivery.UploadCnh;
using Microsoft.AspNetCore.Mvc;

namespace Api.UseCases.Delivery.UploadCnh;

[ApiController]
[Route("entregadores")]
public class DeliveryController(UploadCnhUseCase @case) : ControllerBase
{
    [HttpPost("{id:guid}/cnh")]
    public IActionResult UploadCnh(Guid id, [FromBody] DeliveryUploadCnhBody body)
    {
        var result = @case.Execute(new(id, body.Imagem_cnh ?? ""));

        return this.ToActionResult(result);
    }
}