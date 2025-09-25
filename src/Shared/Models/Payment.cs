using Shared.Abstracts;
using System;

namespace Shared.Models;

public class Payment : Entity
{
    public RentalSubscription Subscription { get; private set; }
    public decimal DailyRate => Subscription.DailyCost;
    public decimal TotalAmount { get; private set; }
    public decimal PenaltyAmount { get; private set; }
    public decimal ExtraDaysAmount { get; private set; }
    public DateTime PaymentDate { get; private set; }

    public Payment(RentalSubscription subscription)
    {
        Subscription = subscription ?? throw new ArgumentNullException(nameof(subscription));
        PaymentDate = DateTime.UtcNow;

        CalculateAmounts();
    }

    public static Payment New(RentalSubscription subscription)
        => new(subscription);

    private void CalculateAmounts()
    {
        if (!Subscription.ReturnDate.HasValue)
            throw new InvalidOperationException("Return date must be set before calculating payment.");

        DateTime returnDate = Subscription.ReturnDate.Value.Date;
        DateTime startDate = Subscription.StartDate.Date;
        DateTime expectedEndDate = Subscription.ExpectedEndDate.Date;

        int totalDaysPlanned = Subscription.RentalPlan.DurationInDays;
        decimal dailyRate = DailyRate;

        if (returnDate < expectedEndDate) // devolução antecipada
        {
            int daysUsed = (returnDate - startDate).Days + 1;
            int unusedDays = totalDaysPlanned - daysUsed;

            TotalAmount = dailyRate * daysUsed;

            decimal penaltyPercent = Subscription.RentalPlan.DurationInDays switch
            {
                7 => 0.20m,
                15 => 0.40m,
                _ => 0m
            };

            PenaltyAmount = unusedDays * dailyRate * penaltyPercent;
            TotalAmount += PenaltyAmount;
        }
        else // devolução no prazo ou atrasada
        {
            TotalAmount = dailyRate * totalDaysPlanned;

            if (returnDate > expectedEndDate) // devolução tardia
            {
                int extraDays = (returnDate - expectedEndDate).Days;
                ExtraDaysAmount = extraDays * 50;
                TotalAmount += ExtraDaysAmount;
            }
        }
    }
}
