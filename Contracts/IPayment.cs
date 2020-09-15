using LittleLogBook.Data.Entities;

using System;

namespace LittleLogBook.Data.Contracts
{
    public interface IPayment
    {
        double Amount { get; set; }

        string CurrencyId { get; set; }

        int CustomerPaymentReferenceNumber { get; }

        DateTime DateTimeRequested { get; }

        DateTime DateTimeResponded { get; set; }

        EnumPaymentGatewayProvider PaymentGatewayProvider { get; }

        Guid PaymentId { get; }

        Guid PaymentReferenceId { get; set; }

        EnumPaymentReferenceType PaymentReferenceType { get; set; }

        EnumPaymentStatus PaymentStatus { get; set; }

        string PaymentStatusMessage { get; set; }

        string Reference { get; set; }

        Guid UserId { get; }

        string AmountText { get; }

        string CustomerPaymentReferenceNumberText { get; }

        string PaymentStatusText { get; }
    }
}