using Api.Extensions;
using Application.UseCases.Motorcycle.Delete;
using Microsoft.AspNetCore.Mvc;

namespace Api.UseCases.Motorcycle.Delete;

[ApiController]
[Route("motos")]
public class MotorcyclesController(DeleteUseCase @case) : ControllerBase
{
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var result = @case.Execute(new(id));

        return this.ToActionResult(result);
    }
}