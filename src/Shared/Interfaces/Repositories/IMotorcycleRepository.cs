using System.Linq.Expressions;
using Shared.Models;

namespace Shared.Interfaces.Repositories;

public interface IMotorcycleRepository
{
    void Add(Motorcycle model);
    Motorcycle[] GetRangeExpress(Expression<Func<Entities.Motorcycle, bool>> func);
    Motorcycle? GetExpress(Expression<Func<Entities.Motorcycle, bool>> func);
    Motorcycle? GetById(Guid id);
    void Update(Motorcycle model);
    void DeleteById(Guid id);
}