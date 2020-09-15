using System;

using LittleLogBook.Data.Entities;

namespace LittleLogBook.Data.Contracts
{
    public interface IReceiptItem
    {
        bool IsCredited { get; }
        
        string ItemDescription { get; }
        
        double NettAmount { get; }
        
        Guid PaymentId { get; }
        
        Guid PaymentReferenceId { get; }
        
        EnumPaymentReferenceType PaymentReferenceType { get; }
        
        double Quantity { get; }
        
        Guid ReceiptItemId { get; }
        
        double TaxRate { get; }
        
        double TotalAmount { get; }
        
        double TotalNettAmount { get; }
        
        double TotalTax { get; }

        Guid CreatedByUserId { get; }
        
        Guid? ViewedByUserId { get; }
        
        DateTime? DateViewed { get; }
    }
}