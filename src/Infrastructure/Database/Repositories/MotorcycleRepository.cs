using System.Linq.Expressions;
using Shared.Interfaces.Repositories;
using Shared.Mappers;
using Shared.Models;

namespace Infrastructure.Database.Repositories;

public class MotorcycleRepository : IMotorcycleRepository
{
    public void Add(Motorcycle model)
    {
        using var context = new Context();

        context.Motorcycles.Add(model.ToEntity());
        context.SaveChanges();
    }

    public void Update(Motorcycle model)
    {
        using var context = new Context();

        context.Motorcycles.Update(model.ToEntity());
        context.SaveChanges();
    }

    public void DeleteById(Guid id)
    {
        using var context = new Context();

        var motorcycle = context.Motorcycles.Find(id);
        if (motorcycle != null)
        {
            context.Motorcycles.Remove(motorcycle);
        }

        context.SaveChanges();
    }

    public Motorcycle? GetExpress(Expression<Func<Shared.Entities.Motorcycle, bool>> func)
    {
        using var context = new Context();

        var entity = context.Motorcycles.FirstOrDefault(func);

        if (entity == null) return null;

        return entity.ToDomain();
    }

    public Motorcycle? GetById(Guid id)
    {
        using var context = new Context();

        var entity = context.Motorcycles.Find(id);

        if (entity == null) return null;

        return entity.ToDomain();
    }

    public Motorcycle[] GetRangeExpress(Expression<Func<Shared.Entities.Motorcycle, bool>> func)
    {
        using var context = new Context();

        var entities = context.Motorcycles
            .Where(func)
            .ToArray();

        return entities.ToDomain();
    }
}