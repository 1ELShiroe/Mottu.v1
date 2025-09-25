using Api.Extensions;
using Application.UseCases.Motorcycle.UpdatePlate;
using Microsoft.AspNetCore.Mvc;

namespace Api.UseCases.Motorcycle.UpdatePlate;

[ApiController]
[Route("motos")]
public class MotorcyclesController(
    UpdatePlateUseCase @case) : ControllerBase
{
    [HttpPut("{id:guid}/plate")]
    public IActionResult UpdatePlate([FromBody] MotorcycleUpdatePlateBody body, Guid id)
    {
        var result = @case.Execute(new(id, body.Placa ?? ""));

        return this.ToActionResult(result);
    }
}