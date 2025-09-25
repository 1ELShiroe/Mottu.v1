using Shared.Models;

namespace Shared.Mappers;

public static class DeliveryMapper
{
    public static Entities.Delivery ToEntity(this Delivery model)
        => new()
        {
            Id = model.Id,
            Name = model.Name,
            Cnpj = model.Cnpj,
            CnhNumber = model.CnhNumber,
            CnhImage = model.CnhImage,
            CnhType = model.CnhType,
            BirthDate = model.BirthDate,
        };

    public static Delivery ToDomain(this Entities.Delivery entity)
    {
        var model = Delivery.New(
            entity.Name,
            entity.Cnpj,
            entity.BirthDate,
            entity.CnhNumber,
            entity.CnhType,
            entity.CnhImage);

        model.SetId(entity.Id);
        return model;
    }

    public static Delivery[] ToDomain(this IEnumerable<Entities.Delivery> entities)
        => [.. entities.Select(e => e.ToDomain())];
}