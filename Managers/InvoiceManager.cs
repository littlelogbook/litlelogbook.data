using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities;
using LittleLogBook.Data.SqlConnectivity;

namespace LittleLogBook.Data.Managers
{
    public class InvoiceManager : ManagerBase, IInvoiceManager
    {
        private IPaymentManager _paymentManager;
        private IReceiptManager _receiptManager;

        internal InvoiceManager(IDataHandler dataHandler, IUser currentUser)
            : base(currentUser)
        {
            _paymentManager = new PaymentManager(dataHandler, CurrentUser);
            _receiptManager = new ReceiptManager(dataHandler, CurrentUser);
        }

        public async Task<bool> RefundPaymentAsync(Guid paymentId)
        {
            return await _paymentManager.RefundPaymentAsync(paymentId);
        }

        public async Task<bool> PaymentExceptionAsync(Guid paymentId, Exception failException)
        {
            return await _paymentManager.PaymentExceptionAsync(paymentId, failException);
        }

        public async Task<bool> PaymentFailedAsync(Guid paymentId, string reference, EnumPaymentStatus paymentStatus, string paymentStatusText)
        {
            return await _paymentManager.PaymentFailedAsync(paymentId, reference, paymentStatus, paymentStatusText);
        }

        public async Task<bool> PaymentCompletedAsync(Guid paymentId, string reference, EnumPaymentStatus paymentStatus)
        {
            return await _paymentManager.PaymentCompletedAsync(paymentId, reference, paymentStatus);
        }

        public async Task<bool> PaymentRedirectedAsync(Guid paymentId, string reference)
        {
            return await _paymentManager.PaymentRedirectedAsync(paymentId, reference);
        }

        public async Task<IPayment> PaymentRequestedAsync(Guid userId, EnumPaymentGatewayProvider paymentGatewayProvider,
            string reference, Guid paymentReferenceId, EnumPaymentReferenceType paymentReferenceType, ICurrency currency,
            double amount, string userIpAddress, ICountry userCountry, string userReferer, string userAgent, string userHost)
        {
            return await _paymentManager.PaymentRequestedAsync(userId, paymentGatewayProvider, reference, paymentReferenceId,
                paymentReferenceType, currency, amount, userIpAddress, userCountry, userReferer, userAgent, userHost);
        }

        public async Task<IEnumerable<IPayment>> GetPaymentsAsync(Guid? userId = null, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            return await _paymentManager.GetPaymentsAsync(userId, dateFrom, dateTo);
        }

        public async Task<IPayment> GetPaymentAsync(Guid paymentId)
        {
            return await _paymentManager.GetPaymentAsync(paymentId);
        }

        public async Task<IPayment> GetPaymentByReferenceAsync(string reference)
        {
            return await _paymentManager.GetPaymentByReferenceAsync(reference);
        }

        public async Task CreateReceiptItemsAsync(IReceiptItem[] receiptItems)
        {
            await _receiptManager.CreateReceiptItemsAsync(receiptItems);
        }

        public async Task<IEnumerable<IReceiptItem>> GetReceiptItemsAsync(Guid paymentId)
        {
            return await _receiptManager.GetReceiptItemsAsync(paymentId);
        }
    }
}
