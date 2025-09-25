using System.Linq.Expressions;
using Shared.Models;

namespace Shared.Interfaces.Repositories;

public interface IRentalSubscriptionRepository
{
    void Add(RentalSubscription model);
    void Update(RentalSubscription model);
    RentalSubscription? GetById(Guid id);
    RentalSubscription? GetExpress(Expression<Func<Entities.RentalSubscription, bool>> func);
}