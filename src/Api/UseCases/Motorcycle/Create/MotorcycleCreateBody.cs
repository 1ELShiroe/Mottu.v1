using System.Text.Json.Serialization;

namespace Api.UseCases.Motorcycle.Create;

public class MotorcycleCreateBody
{
    [JsonPropertyName("ano")]
    public int Year { get; set; }
    [JsonPropertyName("Modelo")]
    public string Model { get; set; } = string.Empty;
    [JsonPropertyName("placa")]
    public string Plate { get; set; } = string.Empty;
}