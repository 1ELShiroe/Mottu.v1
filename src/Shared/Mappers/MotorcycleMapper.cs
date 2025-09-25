using Shared.Dtos;
using Shared.Models;

namespace Shared.Mappers;

public static class MotorcycleMapper
{
    public static Entities.Motorcycle ToEntity(this Motorcycle model)
            => new()
            {
                Id = model.Id,
                Plate = model.Plate,
                Model = model.Model,
                Year = model.Year,
            };

    public static Motorcycle ToDomain(this Entities.Motorcycle entity)
    {
        var model = Motorcycle.New(entity.Year, entity.Model, entity.Plate);
        model.SetId(entity.Id);
        return model;
    }

    public static Motorcycle[] ToDomain(this IEnumerable<Entities.Motorcycle> entities)
        => [.. entities.Select(e => e.ToDomain())];

    public static MotorcycleDto ToDto(this Motorcycle model)
       => new(model.Id, model.Year, model.Model, model.Plate);

    public static MotorcycleDto[] ToDto(this IEnumerable<Motorcycle> models)
        => [.. models.Select(m => m.ToDto())];
}