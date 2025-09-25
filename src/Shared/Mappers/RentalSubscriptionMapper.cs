using Shared.Dtos;
using Shared.Models;

namespace Shared.Mappers;

public static class RentalSubscriptionMapper
{
    public static Entities.RentalSubscription ToEntity(this RentalSubscription model)
        => new()
        {
            Id = model.Id,
            DeliveryId = model.DeliveryId,
            MotorcycleId = model.MotorcycleId,
            RentalPlanId = model.RentalPlanId,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            ExpectedEndDate = model.ExpectedEndDate,
            DailyCost = model.DailyCost,
            TotalCost = model.TotalCost,
            ReturnDate = model.ReturnDate
        };

    public static RentalSubscription ToDomain(this Entities.RentalSubscription entity)
    {
        var model = RentalSubscription.New(
            entity.DeliveryId,
            entity.MotorcycleId,
            entity.RentalPlanId,
            entity.Plan.ToDomain(),
            entity.EndDate
        );

        model.SetId(entity.Id);
        
        if (entity.ReturnDate.HasValue)
        {
            model.SetReturnDate(entity.ReturnDate.Value);
        }

        return model;
    }

    public static RentalSubscriptionDto ToDto(this RentalSubscription entity)
       => new(
           entity.Id,
           entity.DailyCost,
           entity.DeliveryId,
           entity.MotorcycleId,
           entity.StartDate.ToUniversalTime(),
           entity.EndDate.ToUniversalTime().Date.AddDays(1).AddTicks(-1),
           entity.ExpectedEndDate.ToUniversalTime().Date.AddDays(1).AddTicks(-1),
           null
       );
}
