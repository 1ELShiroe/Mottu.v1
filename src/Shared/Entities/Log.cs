using MongoDB.Bson;
using Shared.Enums;

namespace Shared.Entities;

public class Log
{
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    public LogStackLevel Level { get; set; }
    public string? Source { get; set; }
    public string? Action { get; set; }
    public string? Message { get; set; }
    public string? Payload { get; set; }
    public string? CorrelationId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}