using Api.Extensions;
using Application.UseCases.Motorcycle.GetById;
using Microsoft.AspNetCore.Mvc;

namespace Api.UseCases.Motorcycle.GetById;

[ApiController]
[Route("motos")]
public class MotorcyclesController(
    GetByIdUseCase @case) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var result = @case.Execute(new(id));

        return this.ToActionResult(result);
    }
}