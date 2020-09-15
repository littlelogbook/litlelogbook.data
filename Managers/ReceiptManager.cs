using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities;
using LittleLogBook.Data.SqlConnectivity;

namespace LittleLogBook.Data.Managers
{
    public class ReceiptManager : IReceiptManager
    {
        private readonly IDataHandler _dataHandler;
        private readonly IUser _currentUser;

        public ReceiptManager(IDataHandler dataHandler, IUser currentUser)
        {
            _dataHandler = dataHandler;
            _currentUser = currentUser;
        }

        public async Task<IReceiptItem> GetReceiptItemAsync(Guid receiptItemId)
        {
            using (var command = _dataHandler.CreateCommand("GetReceiptItem"))
            {
                command.AddParameter("@ReceiptItemId", receiptItemId, DbType.Guid);
                command.AddParameter("@ViewedByUserId", _currentUser.CloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ReceiptItem(_currentUser.CloudUserId, reader);
                    }
                }
            }

            return null;
        }

        public async Task<IEnumerable<IReceiptItem>> GetReceiptItemsAsync(Guid paymentId)
        {
            var returnValues = new List<IReceiptItem>();

            using (var command = _dataHandler.CreateCommand("GetReceiptItems"))
            {
                command.AddParameter("@PaymentId", paymentId, DbType.Guid);
                command.AddParameter("@ViewedByUserId", _currentUser.CloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        returnValues.Add(new ReceiptItem(_currentUser.CloudUserId, reader));
                    }
                }
            }

            return returnValues;
        }

        public async Task CreateReceiptItemsAsync(IReceiptItem[] receiptItems)
        {
            if (receiptItems != null && receiptItems.Length > 0)
            {
                using (var command = _dataHandler.CreateCommand("InsertReceiptItem"))
                {
                    foreach (var receiptItem in receiptItems)
                    {
                        command.Parameters.Clear();

                        command.AddParameter("@ReceiptItemId", receiptItem.ReceiptItemId, DbType.Guid, ParameterDirection.Input);
                        command.AddParameter("@PaymentId", receiptItem.PaymentId, DbType.Guid, ParameterDirection.Input);
                        command.AddParameter("@PaymentReferenceId", receiptItem.PaymentReferenceId, DbType.Guid, ParameterDirection.Input);
                        command.AddParameter("@PaymentReferenceTypeId", (int)receiptItem.PaymentReferenceType, DbType.Int32, ParameterDirection.Input);
                        command.AddParameter("@ItemDescription", receiptItem.ItemDescription, DbType.String, ParameterDirection.Input);
                        command.AddParameter("@NettAmount", receiptItem.NettAmount, DbType.Decimal, ParameterDirection.Input);
                        command.AddParameter("@Quantity", receiptItem.Quantity, DbType.Decimal, ParameterDirection.Input);
                        command.AddParameter("@TotalNettAmount", receiptItem.TotalNettAmount, DbType.Decimal, ParameterDirection.Input);
                        command.AddParameter("@TaxRate", receiptItem.TaxRate, DbType.Decimal, ParameterDirection.Input);
                        command.AddParameter("@TotalAmount", receiptItem.TotalAmount, DbType.Decimal, ParameterDirection.Input);
                        command.AddParameter("@TotalTax", receiptItem.TotalTax, DbType.Decimal, ParameterDirection.Input);
                        command.AddParameter("@CreatedByUserId", receiptItem.CreatedByUserId, DbType.Guid, ParameterDirection.Input);

                        if ((await command.ExecuteNonQueryAsync()) > 0)
                        {
                            ((ReceiptItem)receiptItem).SetInternals(false, false, DateTime.UtcNow, _currentUser.CloudUserId);
                        }
                    }
                }
            }
        }
    }
}
