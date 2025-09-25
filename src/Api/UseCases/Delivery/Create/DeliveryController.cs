using Api.Extensions;
using Application.UseCases.Delivery.Create;
using Microsoft.AspNetCore.Mvc;

namespace Api.UseCases.Delivery.Create;

[ApiController]
[Route("entregadores")]
public class DeliveryController(CreateUseCase @case) : ControllerBase
{
    [HttpPost]
    public IActionResult Create([FromBody] DeliveryCreateBody body)
    {
        var result = @case.Execute(new(
            body.Nome,
            body.Cnpj,
            body.DataNascimento,
            body.NumeroCnh,
            body.TipoCnh));

        return this.ToActionResult(result);
    }
}