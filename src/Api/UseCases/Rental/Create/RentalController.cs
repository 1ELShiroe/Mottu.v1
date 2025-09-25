using Api.Extensions;
using Application.UseCases.Rental.Create;
using Microsoft.AspNetCore.Mvc;

namespace Api.UseCases.Rental.Create;

[ApiController]
[Route("locacao")]
public class RentalController(CreateUseCase @case) : ControllerBase
{
    [HttpPost]
    public IActionResult Create([FromBody] RentalCreateBody body)
    {
        var request = new CreateUseCaseRequest(
            body.DeliveryId,
            body.MotorcycleId,
            body.StartDate,
            body.EndDate,
            body.ExpectedEndDate,
            body.Plan);

        var result = @case.Execute(request);

        return this.ToActionResult(result);
    }
}