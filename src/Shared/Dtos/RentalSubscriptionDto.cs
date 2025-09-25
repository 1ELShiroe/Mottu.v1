using System;
using System.Text.Json.Serialization;

namespace Shared.Dtos;

public sealed record RentalSubscriptionDto(
    [property: JsonPropertyName("identificador")] Guid Id,
    [property: JsonPropertyName("valor_diaria")] decimal DailyCost,
    [property: JsonPropertyName("entregador_id")] Guid DeliveryId,
    [property: JsonPropertyName("moto_id")] Guid MotorcycleId,
    [property: JsonPropertyName("data_inicio")] DateTime StartDate,
    [property: JsonPropertyName("data_termino")] DateTime EndDate,
    [property: JsonPropertyName("data_previsao_termino")] DateTime ExpectedEndDate,
    [property: JsonPropertyName("data_devolucao")] DateTime? ReturnDate
);
