using Shared.Enums;
using Shared.Models;

namespace Shared.Abstracts;

public abstract class LogRequest
{
    private readonly List<Log> _logs;

    protected LogRequest() => _logs = [];

    public IReadOnlyList<Log> Logs => _logs;

    protected void AddLog(string message, LogStackLevel level)
    {
        _logs.Add(Log.New(level, message));
    }

    public void AddInfo(string message) => AddLog(message, LogStackLevel.INFO);
    public void AddWarning(string message) => AddLog(message, LogStackLevel.WARN);
    public void AddError(string message) => AddLog(message, LogStackLevel.ERROR);

    public IEnumerable<Log> InfoLogs => _logs.Where(l => l.Level == LogStackLevel.INFO);
    public IEnumerable<Log> WarningLogs => _logs.Where(l => l.Level == LogStackLevel.WARN);
    public IEnumerable<Log> ErrorLogs => _logs.Where(l => l.Level == LogStackLevel.ERROR);

    public bool HasErrors => ErrorLogs.Any();
}
