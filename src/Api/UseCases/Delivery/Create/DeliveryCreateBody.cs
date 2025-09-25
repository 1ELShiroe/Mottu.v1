using System.Text.Json.Serialization;

namespace Api.UseCases.Delivery.Create;

public class DeliveryCreateBody
{
    [JsonPropertyName("nome")]
    public string Nome { get; set; } = null!;

    [JsonPropertyName("cnpj")]
    public string Cnpj { get; set; } = null!;

    [JsonPropertyName("data_nascimento")]
    public DateTime DataNascimento { get; set; }

    [JsonPropertyName("numero_cnh")]
    public string NumeroCnh { get; set; } = null!;

    [JsonPropertyName("tipo_cnh")]
    public string TipoCnh { get; set; } = null!;
}
