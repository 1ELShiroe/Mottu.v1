using Shared.Abstracts;

namespace Shared.Models;

public class RentalSubscription : Entity
{
    public Guid DeliveryId { get; private set; }
    public Guid MotorcycleId { get; private set; }
    public Guid RentalPlanId { get; private set; }

    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public DateTime ExpectedEndDate { get; private set; }
    public DateTime? ReturnDate { get; private set; }
    public RentalPlan RentalPlan { get; private set; }

    public decimal DailyCost { get; private set; }
    public decimal TotalCost { get; private set; }

    public RentalSubscription(
        Guid deliveryId,
        Guid motorcycleId,
        Guid rentalPlanId,
        DateTime startDate,
        DateTime endDate,
        DateTime expectedEndDate,
        RentalPlan rentalPlan,
        decimal dailyCost)
    {
        DeliveryId = deliveryId;
        MotorcycleId = motorcycleId;
        RentalPlanId = rentalPlanId;

        StartDate = startDate;
        EndDate = endDate;
        ExpectedEndDate = expectedEndDate;
        RentalPlan = rentalPlan;

        DailyCost = dailyCost;
        TotalCost = CalculateTotalCost();
    }

    public static RentalSubscription New(
        Guid deliveryId,
        Guid motorcycleId,
        Guid rentalPlanId,
        RentalPlan plan,
        DateTime creationDate)
    {
        var startDate = creationDate.AddDays(1);
        var endDate = startDate.AddDays(plan.DurationInDays - 1);
        var expectedEndDate = endDate;

        return new RentalSubscription(
            deliveryId,
            motorcycleId,
            rentalPlanId,
            startDate,
            endDate,
            expectedEndDate,
            plan,
            plan.DailyCost);
    }

    public void SetReturnDate(DateTime date) => ReturnDate = date;

    private decimal CalculateTotalCost() => DailyCost * (EndDate - StartDate).Days + 1;
}
