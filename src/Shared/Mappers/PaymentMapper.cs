using Shared.Dtos;
using Shared.Models;

namespace Shared.Mappers;

public static class PaymentMapper
{
    public static PaymentDto ToDto(this Payment payment)
       => new(
           payment.Id,
           payment.DailyRate,
           payment.TotalAmount,
           payment.PenaltyAmount,
           payment.ExtraDaysAmount,
           payment.PaymentDate,
           payment.Subscription.StartDate,
           payment.Subscription.EndDate,
           payment.Subscription.ExpectedEndDate,
           payment.Subscription.ReturnDate!.Value
       );
}