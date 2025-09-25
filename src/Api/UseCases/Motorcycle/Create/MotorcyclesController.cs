using Api.Extensions;
using Application.UseCases.Motorcycle.Create;
using Microsoft.AspNetCore.Mvc;

namespace Api.UseCases.Motorcycle.Create;

[ApiController]
[Route("motos")]
public class MotorcyclesController(
    CreateUseCase @case) : ControllerBase
{
    [HttpPost]
    public IActionResult Create([FromBody] MotorcycleCreateBody body)
    {
        var result = @case.Execute(new(body.Year, body.Model, body.Plate));

        return this.ToActionResult(result);
    }
}