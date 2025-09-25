using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Entities;

public class RentalSubscription
{
    public Guid Id { get; set; }
    public Guid DeliveryId { get; set; }
    public Guid MotorcycleId { get; set; }
    public Guid RentalPlanId { get; set; }

    public RentalPlan Plan { get; set; }
    public Motorcycle Motorcycle { get; set; }
    public Delivery Delivery { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime ExpectedEndDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public decimal DailyCost { get; set; }
    public decimal TotalCost { get; set; }
}