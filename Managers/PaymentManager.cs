using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities;
using LittleLogBook.Data.SqlConnectivity;

namespace LittleLogBook.Data.Managers
{
    public class PaymentManager : IPaymentManager
    {
        private readonly IDataHandler _dataHandler;
        private readonly ICloudUser _currentUser;

        public PaymentManager(IDataHandler dataHandler, ICloudUser currentUser)
        {
            _dataHandler = dataHandler;
            _currentUser = currentUser;
        }

        public async Task<IEnumerable<Payment>> GetPaymentsAsync(Guid? userId = null, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            var returnValues = new List<Payment>();

            using (var command = _dataHandler.CreateCommand("GetPayments"))
            {
                command.AddParameter("@ViewedByUserId", _currentUser.CloudUserId, DbType.Guid);
                command.AddParameter("@UserId", userId, DbType.Guid);
                command.AddParameter("@DateFrom", dateFrom, DbType.DateTime);
                command.AddParameter("@DateTo", dateTo, DbType.DateTime);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        returnValues.Add(new Payment(_currentUser.CloudUserId, reader));
                    }
                }
            }

            return returnValues;
        }

        public async Task<Payment> GetPaymentAsync(Guid paymentId)
        {
            if (Guid.Empty.Equals(paymentId))
            {
                throw new ArgumentNullException(nameof(paymentId), "The payment ID was not specified");
            }

            using (var command = _dataHandler.CreateCommand("GetPayment"))
            {
                command.AddParameter("@ViewedByUserId", _currentUser.CloudUserId, DbType.Guid);
                command.AddParameter("@PaymentId", paymentId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Payment(_currentUser.CloudUserId, reader);
                    }
                }
            }

            return null;
        }

        public async Task<Payment> GetPaymentByReferenceAsync(string reference)
        {
            if (string.IsNullOrWhiteSpace(reference))
            {
                throw new ArgumentNullException(nameof(reference), "The specified reference was not specified");
            }

            using (var command = _dataHandler.CreateCommand("GetPaymentByReference"))
            {
                command.AddParameter("@ViewedByUserId", _currentUser.CloudUserId, DbType.Guid);
                command.AddParameter("@Reference", reference, DbType.String);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Payment(_currentUser.CloudUserId, reader);
                    }
                }
            }

            return null;
        }

        public async Task<Payment> PaymentRequestedAsync(Guid userId, EnumPaymentGatewayProvider paymentGatewayProvider,
            string reference, Guid paymentReferenceId, EnumPaymentReferenceType paymentReferenceType, ICurrency currency,
            double amount, string userIpAddress, ICountry userCountry, string userReferer, string userAgent, string userHost)
        {
            var payment = new Payment(_currentUser.CloudUserId, userId, paymentGatewayProvider, reference, paymentReferenceId,
                paymentReferenceType, currency.CurrencyId, currency.ExchangeRate, amount);

            using (var command = _dataHandler.CreateCommand("PaymentRequested"))
            {
                command
                    .AddParameter("@PaymentId", payment.PaymentId, DbType.Guid)
                    .AddParameter("@UserId", userId, DbType.Guid)
                    .AddParameter("@PaymentGatewayProviderId", (int) paymentGatewayProvider, DbType.Int32)
                    .AddParameter("@Reference", reference, DbType.String)
                    .AddParameter("@PaymentReferenceId", paymentReferenceId, DbType.Guid)
                    .AddParameter("@PaymentReferenceTypeId", (int) paymentReferenceType, DbType.Int32)
                    .AddParameter("@CurrencyId", currency.CurrencyId, DbType.String)
                    .AddParameter("@ExchangeRate", currency.ExchangeRate, DbType.Decimal)
                    .AddParameter("@Amount", amount, DbType.Decimal)
                    .AddParameter("@UserIpAddress", userIpAddress, DbType.String)
                    .AddParameter("@UserCountryId", userCountry?.CountryId, DbType.Int32)
                    .AddParameter("@UserCountryIso3", userCountry?.Iso3, DbType.String)
                    .AddParameter("@UserReferer", userReferer, DbType.String)
                    .AddParameter("@UserAgent", userAgent, DbType.String)
                    .AddParameter("@UserHost", userHost, DbType.String)
                    .AddParameter("@CreatedByUserId", _currentUser.CloudUserId, DbType.Guid)
                    .AddParameter("@CustomerPaymentReferenceNumber", null, DbType.Int32, ParameterDirection.Output);

                if (await command.ExecuteNonQueryAsync() > 0)
                {
                    payment.SetInternals(false, false, DateTime.UtcNow, _currentUser.CloudUserId);
                    payment.CustomerPaymentReferenceNumber = (int) command.Parameters["@CustomerPaymentReferenceNumber"].Value;
                    payment.ViewedByUserId = _currentUser.CloudUserId;
                    payment.DateViewed = DateTime.UtcNow;
                }
            }

            return payment;
        }

        public async Task<bool> PaymentCompletedAsync(Guid paymentId, string reference, EnumPaymentStatus paymentStatus)
        {
            using (var command = _dataHandler.CreateCommand("PaymentCompleted"))
            {
                command
                    .AddParameter("@PaymentId", paymentId, DbType.Guid)
                    .AddParameter("@Reference", reference, DbType.String)
                    .AddParameter("@PaymentStatusId", (int) paymentStatus, DbType.Int32)
                    .AddParameter("@PaymentStatus", paymentStatus.ToString(), DbType.String)
                    .AddParameter("@ModifiedByUserId", _currentUser.CloudUserId, DbType.Guid);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<bool> PaymentRedirectedAsync(Guid paymentId, string reference)
        {
            using (var command = _dataHandler.CreateCommand("PaymentRedirected"))
            {
                command
                    .AddParameter("@PaymentId", paymentId, DbType.Guid)
                    .AddParameter("@Reference", reference, DbType.String)
                    .AddParameter("@ModifiedByUserId", _currentUser.CloudUserId, DbType.Guid);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<bool> PaymentFailedAsync(Guid paymentId, string reference, EnumPaymentStatus paymentStatus, string paymentStatusText)
        {
            using (var command = _dataHandler.CreateCommand("PaymentFailed"))
            {
                command
                    .AddParameter("@PaymentId", paymentId, DbType.Guid)
                    .AddParameter("@Reference", reference, DbType.String)
                    .AddParameter("@PaymentStatus", paymentStatusText, DbType.String)
                    .AddParameter("@PaymentStatusId", (int) paymentStatus, DbType.Int32)
                    .AddParameter("@ModifiedByUserId", _currentUser.CloudUserId, DbType.Guid);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<bool> PaymentExceptionAsync(Guid paymentId, Exception failException)
        {
            using (var command = _dataHandler.CreateCommand("PaymentError"))
            {
                command
                    .AddParameter("@PaymentId", paymentId, DbType.String)
                    .AddParameter("@ExceptionMessage", failException.Message, DbType.String)
                    .AddParameter("@ModifiedByUserId", _currentUser.CloudUserId, DbType.Guid);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<bool> RefundPaymentAsync(Guid paymentId)
        {
            using (var command = _dataHandler.CreateCommand("RefundPayment"))
            {
                command
                    .AddParameter("@PaymentId", paymentId, DbType.Guid)
                    .AddParameter("@ModifiedByUserId", _currentUser.CloudUserId, DbType.Guid);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }
    }
}