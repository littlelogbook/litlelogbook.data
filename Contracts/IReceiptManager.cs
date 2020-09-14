using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using LittleLogBook.Data.Entities;

namespace LittleLogBook.Data.Contracts
{
    public interface IReceiptManager
    {
        Task<ReceiptItem> GetReceiptItemAsync(Guid receiptItemId);

        Task<IEnumerable<ReceiptItem>> GetReceiptItemsAsync(Guid paymentId);

        Task CreateReceiptItemsAsync(ReceiptItem[] receiptItems);
    }
}
