using Shared.Abstracts;

namespace Shared.Models;

public class RentalPlan : Entity
{
    public int DurationInDays { get; private set; }
    public decimal DailyCost { get; private set; }

    public RentalPlan(int durationInDays, decimal dailyCost)
    {
        DurationInDays = durationInDays;
        DailyCost = dailyCost;
    }

    public static RentalPlan New(int durationInDays, decimal dailyCost)
        => new(durationInDays, dailyCost);
}