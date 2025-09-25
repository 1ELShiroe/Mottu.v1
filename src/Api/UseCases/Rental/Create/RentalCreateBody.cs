using System;
using System.Text.Json.Serialization;

namespace Api.UseCases.Rental.Create;

public class RentalCreateBody
{
    [JsonPropertyName("entregador_id")]
    public Guid DeliveryId { get; set; }

    [JsonPropertyName("moto_id")]
    public Guid MotorcycleId { get; set; }

    [JsonPropertyName("data_inicio")]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("data_termino")]
    public DateTime EndDate { get; set; }

    [JsonPropertyName("data_previsao_termino")]
    public DateTime ExpectedEndDate { get; set; }

    [JsonPropertyName("plano")]
    public int Plan { get; set; }
}
