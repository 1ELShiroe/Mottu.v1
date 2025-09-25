using MongoDB.Bson;
using Shared.Enums;

namespace Shared.Models;

public class Log
{
    public string Id { get; private set; }
    public LogStackLevel Level { get; private set; }
    public string? Source { get; private set; }
    public string? Action { get; private set; }
    public string? Message { get; private set; }
    public string? Payload { get; private set; }
    public string? CorrelationId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Log(
        LogStackLevel level, string? source = null, string? action = null,
        string? message = null, string? payload = null, string? correlationId = null)
    {
        Id = ObjectId.GenerateNewId().ToString();
        Level = level;
        Source = source;
        Action = action;
        Message = message;
        Payload = payload;
        CorrelationId = correlationId;
        CreatedAt = DateTime.UtcNow;
    }

    public static Log New(
        LogStackLevel level, string? source = null, string? action = null,
        string? message = null, string? payload = null, string? correlationId = null)
        => new(level, source, action, message, payload, correlationId);
}
