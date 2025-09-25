using System.Linq.Expressions;
using Shared.Models;

namespace Shared.Interfaces.Repositories;

public interface IRentalPlanRepository
{
    RentalPlan? GetById(Guid id);
    RentalPlan? GetExpress(Expression<Func<Entities.RentalPlan, bool>> func);
    void Add(RentalPlan model);
}