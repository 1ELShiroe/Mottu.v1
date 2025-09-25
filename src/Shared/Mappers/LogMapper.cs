using Shared.Dtos;
using Shared.Models;

namespace Shared.Mappers;

public static class LogMapper
{
    public static LogDto ToDto(this Log log)
    {
        if (log == null) return null!;

        return new LogDto
        {
            Id = log.Id,
            Level = log.Level,
            Source = log.Source,
            Action = log.Action,
            Message = log.Message,
            Payload = log.Payload,
            CorrelationId = log.CorrelationId,
            CreatedAt = log.CreatedAt
        };
    }

    public static LogDto ToDto(this Entities.Log log)
    {
        if (log == null) return null!;

        return new LogDto
        {
            Id = log.Id,
            Level = log.Level,
            Source = log.Source,
            Action = log.Action,
            Message = log.Message,
            Payload = log.Payload,
            CorrelationId = log.CorrelationId,
            CreatedAt = log.CreatedAt
        };
    }


    public static Log ToDomain(this Entities.Log dto)
    {
        if (dto == null) return null!;

        return Log.New(
            dto.Level,
            dto.Source,
            dto.Action,
            dto.Message,
            dto.Payload,
            dto.CorrelationId
        );
    }

    public static Log ToDomain(this LogDto dto)
    {
        if (dto == null) return null!;

        return Log.New(
            dto.Level,
            dto.Source,
            dto.Action,
            dto.Message,
            dto.Payload,
            dto.CorrelationId
        );
    }

    public static Entities.Log ToEntity(this LogDto log)
    {
        if (log == null) return null!;

        return new Entities.Log
        {
            Id = log.Id,
            Level = log.Level,
            Source = log.Source,
            Action = log.Action,
            Message = log.Message,
            Payload = log.Payload,
            CorrelationId = log.CorrelationId,
            CreatedAt = log.CreatedAt
        };
    }

    public static Entities.Log ToEntity(this Log log)
    {
        if (log == null) return null!;

        return new Entities.Log
        {
            Id = log.Id,
            Level = log.Level,
            Source = log.Source,
            Action = log.Action,
            Message = log.Message,
            Payload = log.Payload,
            CorrelationId = log.CorrelationId,
            CreatedAt = log.CreatedAt
        };
    }
}