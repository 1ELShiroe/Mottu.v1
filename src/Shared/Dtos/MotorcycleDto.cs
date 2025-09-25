using System.Text.Json.Serialization;

namespace Shared.Dtos;

public sealed record MotorcycleDto(
    [property: JsonPropertyName("identificador")] Guid Id,
    [property: JsonPropertyName("ano")] int Year,
    [property: JsonPropertyName("modelo")] string Model,
    [property: JsonPropertyName("placa")] string Plate
);
