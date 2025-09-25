using Api.Extensions;
using Application.UseCases.Rental.GetById;
using Microsoft.AspNetCore.Mvc;

namespace Api.UseCases.Rental.GetById;

[ApiController]
[Route("locacao")]
public class RentalController(GetByIdUseCase @case) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var result = @case.Execute(new(id));

        return this.ToActionResult(result);
    }
}