using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities;
using LittleLogBook.Data.SqlConnectivity;

namespace LittleLogBook.Data.Managers
{
    public class ReceiptManager : ManagerBase, IReceiptManager
    {
        private readonly IDataHandler _dataHandler;

        internal ReceiptManager(IDataHandler dataHandler, IUser currentUser)
            : base(currentUser)
        {
            _dataHandler = dataHandler;
        }

        public async Task<IReceiptItem> GetReceiptItemAsync(Guid receiptItemId)
        {
            using (var command = _dataHandler.CreateCommand("GetReceiptItem"))
            {
                command.AddParameter("@ReceiptItemId", receiptItemId, DbType.Guid);
                command.AddParameter("@ViewedByUserId", CurrentUser.CloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ReceiptItem(CurrentUser.CloudUserId, reader);
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
                command.AddParameter("@ViewedByUserId", CurrentUser.CloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        returnValues.Add(new ReceiptItem(CurrentUser.CloudUserId, reader));
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

                        command.AddParameter("@ReceiptItemId", receiptItem.ReceiptItemId, DbType.Guid);
                        command.AddParameter("@PaymentId", receiptItem.PaymentId, DbType.Guid);
                        command.AddParameter("@PaymentReferenceId", receiptItem.PaymentReferenceId, DbType.Guid);
                        command.AddParameter("@PaymentReferenceTypeId", (int)receiptItem.PaymentReferenceType, DbType.Int32);
                        command.AddParameter("@ItemDescription", receiptItem.ItemDescription, DbType.String);
                        command.AddParameter("@NettAmount", receiptItem.NettAmount, DbType.Decimal);
                        command.AddParameter("@Quantity", receiptItem.Quantity, DbType.Decimal);
                        command.AddParameter("@TotalNettAmount", receiptItem.TotalNettAmount, DbType.Decimal);
                        command.AddParameter("@TaxRate", receiptItem.TaxRate, DbType.Decimal);
                        command.AddParameter("@TotalAmount", receiptItem.TotalAmount, DbType.Decimal);
                        command.AddParameter("@TotalTax", receiptItem.TotalTax, DbType.Decimal);
                        command.AddParameter("@CreatedByUserId", receiptItem.CreatedByUserId, DbType.Guid);

                        if ((await command.ExecuteNonQueryAsync()) > 0)
                        {
                            ((ReceiptItem)receiptItem).SetInternals(false, false, DateTime.UtcNow, CurrentUser.CloudUserId);
                        }
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dataHandler?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
