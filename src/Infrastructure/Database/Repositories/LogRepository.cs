using Shared.Interfaces.Repositories;
using Shared.Mappers;
using Shared.Models;

namespace Infrastructure.Database.Repositories;

public class LogRepository : ILogRepository
{
    public void Add(Log log)
    {
        using var context = new MongoContext();

        context.Logs.Add(log.ToEntity());
        context.SaveChanges();
    }

    public void AddRange(List<Log> logs)
    {
        using var context = new MongoContext();

        var convertted = logs
            .Select(l => l.ToEntity())
            .ToList();

        context.Logs.AddRange(convertted);
        context.SaveChanges();
    }
}