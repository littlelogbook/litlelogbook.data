using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using LittleLogBook.Data.Entities;

namespace LittleLogBook.Data.Contracts
{
    public interface IInvoiceManager
    {
        Task CreateReceiptItemsAsync(IReceiptItem[] receiptItems);
        
        Task<IPayment> GetPaymentAsync(Guid paymentId);
        
        Task<IPayment> GetPaymentByReferenceAsync(string reference);

        Task<IEnumerable<IPayment>> GetPaymentsAsync(Guid? userId = null, DateTime? dateFrom = null, DateTime? dateTo = null);
        
        Task<IEnumerable<IReceiptItem>> GetReceiptItemsAsync(Guid paymentId);
        
        Task<bool> PaymentCompletedAsync(Guid paymentId, string reference, EnumPaymentStatus paymentStatus);
        
        Task<bool> PaymentExceptionAsync(Guid paymentId, Exception failException);
        
        Task<bool> PaymentFailedAsync(Guid paymentId, string reference, EnumPaymentStatus paymentStatus, string paymentStatusText);
        
        Task<bool> PaymentRedirectedAsync(Guid paymentId, string reference);
        
        Task<IPayment> PaymentRequestedAsync(Guid userId, EnumPaymentGatewayProvider paymentGatewayProvider, string reference,
            Guid paymentReferenceId, EnumPaymentReferenceType paymentReferenceType, ICurrency currency, double amount,
            string userIpAddress, ICountry userCountry, string userReferer, string userAgent, string userHost);

        Task<bool> RefundPaymentAsync(Guid paymentId);
    }
}