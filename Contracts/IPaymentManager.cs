using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using LittleLogBook.Data.Entities;

namespace LittleLogBook.Data.Contracts
{
    public interface IPaymentManager
    {
        Task<Payment> GetPaymentAsync(Guid paymentId);
        
        Task<Payment> GetPaymentByReferenceAsync(string reference);
        
        Task<IEnumerable<Payment>> GetPaymentsAsync(Guid? userId = null, DateTime? dateFrom = null, DateTime? dateTo = null);
        
        Task<bool> PaymentCompletedAsync(Guid paymentId, string reference, EnumPaymentStatus paymentStatus);
        
        Task<bool> PaymentExceptionAsync(Guid paymentId, Exception failException);
        
        Task<bool> PaymentFailedAsync(Guid paymentId, string reference, EnumPaymentStatus paymentStatus, string paymentStatusText);
        
        Task<bool> PaymentRedirectedAsync(Guid paymentId, string reference);
        
        Task<Payment> PaymentRequestedAsync(Guid userId, EnumPaymentGatewayProvider paymentGatewayProvider, string reference, 
            Guid paymentReferenceId, EnumPaymentReferenceType paymentReferenceType, ICurrency currency, double amount, 
            string userIpAddress, ICountry userCountry, string userReferer, string userAgent, string userHost);
        
        Task<bool> RefundPaymentAsync(Guid paymentId);
    }
}