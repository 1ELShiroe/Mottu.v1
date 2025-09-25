namespace Shared.Entities;

public class RentalPlan
{
    public Guid Id { get; set; }
    public int DurationInDays { get; set; }
    public decimal DailyCost { get; set; }
}