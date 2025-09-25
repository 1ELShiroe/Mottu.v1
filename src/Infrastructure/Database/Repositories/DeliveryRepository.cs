using System.Linq.Expressions;
using Shared.Entities;
using Shared.Interfaces.Repositories;
using Shared.Mappers;

namespace Infrastructure.Database.Repositories;

public class DeliveryRepository : IDeliveryRepository
{
    public void Add(Shared.Models.Delivery model)
    {
        using var context = new Context();

        context.Deliverys.Add(model.ToEntity());
        context.SaveChanges();
    }

    public void Update(Shared.Models.Delivery model)
    {
        using var context = new Context();

        context.Deliverys.Update(model.ToEntity());
        context.SaveChanges();
    }

    public void DeleteById(Guid id)
    {
        using var context = new Context();

        var Delivery = context.Deliverys.Find(id);
        if (Delivery != null)
        {
            context.Deliverys.Remove(Delivery);
        }

        context.SaveChanges();
    }

    public Shared.Models.Delivery? GetExpress(Expression<Func<Delivery, bool>> func)
    {
        using var context = new Context();

        var entity = context.Deliverys.FirstOrDefault(func);

        if (entity == null) return null;

        return entity.ToDomain();
    }

    public Shared.Models.Delivery? GetById(Guid id)
    {
        using var context = new Context();

        var entity = context.Deliverys.Find(id);

        if (entity == null) return null;

        return entity.ToDomain();
    }

    public Shared.Models.Delivery[] GetRangeExpress(Expression<Func<Shared.Entities.Delivery, bool>> func)
    {
        using var context = new Context();

        var entities = context.Deliverys
            .Where(func)
            .ToArray();

        return entities.ToDomain();
    }
}