using System;
using System.Threading.Tasks;

using LittleLogBook.Data.Entities;

namespace LittleLogBook.Data.Contracts
{
    public interface IBasketManager
    {
        Task<bool> AddBasketItem(Guid BasketItemId, Guid UserId, Guid ItemReferenceId, EnumPaymentReferenceType ItemReferenceType,
            double NettAmount, double Quantity, double TotalNettAmount, double TaxRate, double TotalTax, double TotalAmount);
    }
}
