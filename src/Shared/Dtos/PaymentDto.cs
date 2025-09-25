using System.Text.Json.Serialization;

namespace Shared.Dtos;

public sealed record PaymentDto(
    [property: JsonPropertyName("identificador")] Guid Id,
    [property: JsonPropertyName("valor_diaria")] decimal DailyRate,
    [property: JsonPropertyName("total")] decimal TotalAmount,
    [property: JsonPropertyName("multa")] decimal PenaltyAmount,
    [property: JsonPropertyName("dias_adicionais")] decimal ExtraDaysAmount,
    [property: JsonPropertyName("data_pagamento")] DateTime PaymentDate,
    [property: JsonPropertyName("data_inicio")] DateTime StartDate,
    [property: JsonPropertyName("data_termino")] DateTime EndDate,
    [property: JsonPropertyName("data_previsao_termino")] DateTime ExpectedEndDate,
    [property: JsonPropertyName("data_devolucao")] DateTime ReturnDate
);