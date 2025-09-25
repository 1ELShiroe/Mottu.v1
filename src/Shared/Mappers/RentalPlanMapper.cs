using Shared.Models;

namespace Shared.Mappers;

public static class RentalPlanMapper
{
    public static Entities.RentalPlan ToEntity(this RentalPlan model)
        => new()
        {
            Id = model.Id,
            DailyCost = model.DailyCost,
            DurationInDays = model.DurationInDays,
        };

    public static RentalPlan ToDomain(this Entities.RentalPlan entity)
    {
        var model = RentalPlan.New(entity.DurationInDays, entity.DailyCost);
        model.SetId(entity.Id);
        return model;
    }
}