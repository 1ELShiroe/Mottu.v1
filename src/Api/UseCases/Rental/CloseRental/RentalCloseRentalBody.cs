using System.Text.Json.Serialization;

namespace Api.UseCases.Rental.CloseRental;

public class RentalCloseRentalBody
{
    [JsonPropertyName("data_devolucao")]
    public DateTime ReturnDate { get; set; }
}