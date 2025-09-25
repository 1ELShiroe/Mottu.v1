using System.Linq.Expressions;
using Shared.Interfaces.Repositories;
using Shared.Mappers;
using Shared.Models;

namespace Infrastructure.Database.Repositories;

public class RentalPlanRepository : IRentalPlanRepository
{
    public void Add(RentalPlan model)
    {
        using var context = new Context();

        context.RentalPlans.Add(model.ToEntity());
        context.SaveChanges();
    }
    
    public RentalPlan? GetExpress(Expression<Func<Shared.Entities.RentalPlan, bool>> func)
    {
        using var context = new Context();

        var entity = context.RentalPlans.FirstOrDefault(func);

        return entity?.ToDomain();
    }

    public RentalPlan? GetById(Guid id)
    {
        using var context = new Context();

        var entity = context.RentalPlans.Find(id);

        return entity?.ToDomain();
    }
}