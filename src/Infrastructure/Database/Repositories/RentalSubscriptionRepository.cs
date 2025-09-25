using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces.Repositories;
using Shared.Mappers;
using Shared.Models;

namespace Infrastructure.Database.Repositories;

public class RentalSubscriptionRepository : IRentalSubscriptionRepository
{
    public void Add(RentalSubscription model)
    {
        using var context = new Context();

        context.RentalSubscriptions.Add(model.ToEntity());
        context.SaveChanges();
    }

    public void Update(RentalSubscription model)
    {
        using var context = new Context();

        context.RentalSubscriptions.Update(model.ToEntity());
        context.SaveChanges();
    }

    public RentalSubscription? GetExpress(Expression<Func<Shared.Entities.RentalSubscription, bool>> func)
    {
        using var context = new Context();

        var entity = context.RentalSubscriptions
                            .Include(r => r.Plan)
                            .Include(r => r.Motorcycle)
                            .Include(r => r.Delivery)
                            .FirstOrDefault(func);

        return entity?.ToDomain();
    }

    public RentalSubscription? GetById(Guid id)
    {
        using var context = new Context();

        var entity = context.RentalSubscriptions
                .Include(r => r.Plan)
                .Include(r => r.Motorcycle)
                .Include(r => r.Delivery)
                .FirstOrDefault(r => r.Id == id);

        return entity?.ToDomain();
    }
}