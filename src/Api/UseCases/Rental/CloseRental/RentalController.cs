using Api.Extensions;
using Application.UseCases.Rental.CloseRentalUseCase;
using Microsoft.AspNetCore.Mvc;

namespace Api.UseCases.Rental.CloseRental;

[ApiController]
[Route("locacao")]
public class RentalController(CloseRentalUseCase @case) : ControllerBase
{
    [HttpPut("{id:guid}/devolucao")]
    public IActionResult CloseRental([FromBody] RentalCloseRentalBody body, Guid id)
    {
        var result = @case.Execute(new(id, body.ReturnDate));

        return this.ToActionResult(result);
    }
}