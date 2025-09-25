using Shared.Enums;

namespace Shared.Dtos;

public class LogDto
{
    public string Id { get; set; } = default!;
    public LogStackLevel Level { get; set; }
    public string? Source { get; set; }
    public string? Action { get; set; }
    public string? Message { get; set; }
    public string? Payload { get; set; }
    public string? CorrelationId { get; set; }
    public DateTime CreatedAt { get; set; }
}
