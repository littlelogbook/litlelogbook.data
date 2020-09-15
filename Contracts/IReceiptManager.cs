using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using LittleLogBook.Data.Entities;

namespace LittleLogBook.Data.Contracts
{
    public interface IReceiptManager
    {
        Task<IReceiptItem> GetReceiptItemAsync(Guid receiptItemId);

        Task<IEnumerable<IReceiptItem>> GetReceiptItemsAsync(Guid paymentId);

        Task CreateReceiptItemsAsync(IReceiptItem[] receiptItems);
    }
}
