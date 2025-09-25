using Api.Extensions;
using Application.UseCases.Motorcycle.Get;
using Microsoft.AspNetCore.Mvc;

namespace Api.UseCases.Motorcycle.Get;

[ApiController]
[Route("motos")]
public class MotorcyclesController(
    GetUseCase @case) : ControllerBase
{
    [HttpGet]
    public IActionResult Get(
        [FromQuery] string? modelo,
        [FromQuery] string? ano,
        [FromQuery] string? placa)
    {
        var result = @case.Execute(new(ano, modelo, placa));

        return this.ToActionResult(result);
    }
}