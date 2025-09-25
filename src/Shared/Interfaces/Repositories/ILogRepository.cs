using Shared.Models;

namespace Shared.Interfaces.Repositories;

public interface ILogRepository
{
    void Add(Log log);
    void AddRange(List<Log> log);
}