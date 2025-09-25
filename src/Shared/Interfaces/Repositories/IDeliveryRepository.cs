using System.Linq.Expressions;
using Shared.Models;

namespace Shared.Interfaces.Repositories;

public interface IDeliveryRepository
{
    void Add(Delivery model);
    Delivery[] GetRangeExpress(Expression<Func<Entities.Delivery, bool>> func);
    Delivery? GetExpress(Expression<Func<Entities.Delivery, bool>> func);
    Delivery? GetById(Guid id);
    void Update(Delivery model);
    void DeleteById(Guid id);
}