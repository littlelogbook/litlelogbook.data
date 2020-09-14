using System;
using System.Data.SqlClient;

using LittleLogBook.Data.Entities.Base;

namespace LittleLogBook.Data.Entities
{
    public class ReceiptItem : BaseReceiptItem
	{
		public ReceiptItem(Guid CreatedByUserId, Guid PaymentId, Guid PaymentReference, EnumPaymentReferenceType PaymentReferenceType,
			string ItemDescription, double NettAmount, double Quantity, double PriceExTax, double TaxRate, double TotalTax, double TotalAmount)
            : base(CreatedByUserId, PaymentId, PaymentReference, PaymentReferenceType, ItemDescription, NettAmount, Quantity, PriceExTax, TaxRate, TotalTax, TotalAmount)
		{

		}

		public ReceiptItem(Guid ViewedByUserId, SqlDataReader Reader) : base(ViewedByUserId, Reader)
		{

		}
	}
}