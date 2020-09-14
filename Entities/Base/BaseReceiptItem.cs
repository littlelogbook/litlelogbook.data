using System;
using System.Data;
using System.Data.SqlClient;

namespace LittleLogBook.Data.Entities.Base
{
	public class BaseReceiptItem : BaseEntity
	{
        private Guid _receiptItemId = Guid.NewGuid();
        private Guid _paymentId;
        private Guid _paymentReferenceId;
        private EnumPaymentReferenceType _paymentReferenceType;
        private string _itemDescription;
        private double _nettAmount;
        private double _quantity;
        private double _totalNettAmount;
        private double _taxRate;
        private double _totalTax;
        private double _totalAmount;
        private bool _isCredited;

        public Guid ReceiptItemId
        {
            get
            {
                return _receiptItemId;
            }
        }

        public Guid PaymentId
        {
            get
            {
                return _paymentId;
            }
        }

        public Guid PaymentReferenceId
        {
            get
            {
                return _paymentReferenceId;
            }
        }

        public EnumPaymentReferenceType PaymentReferenceType
        {
            get
            {
                return _paymentReferenceType;
            }
        }

        public string ItemDescription
        {
            get
            {
                return _itemDescription;
            }
        }

        public double NettAmount
        {
            get
            {
                return _nettAmount;
            }
        }

        public double Quantity
        {
            get
            {
                return _quantity;
            }
        }

        public double TotalNettAmount
        {
            get
            {
                return _totalNettAmount;
            }
        }

        public double TaxRate
        {
            get
            {
                return _taxRate;
            }
        }

        public double TotalAmount
        {
            get
            {
                return _totalAmount;
            }
        }

        public double TotalTax
        {
            get
            {
                return _totalTax;
            }
        }

        public bool IsCredited
        {
            get
            {
                return _isCredited;
            }
        }

        public BaseReceiptItem(Guid CreatedByUserId, Guid PaymentId, Guid PaymentReference, EnumPaymentReferenceType PaymentReferenceType, string ItemDescription, double NettAmount, double Quantity, double TotalNettAmount, double TaxRate, double TotalTax, double TotalAmount) : base(CreatedByUserId)
		{
            _paymentId = PaymentId;
            _paymentReferenceId = PaymentReference;
            _paymentReferenceType = PaymentReferenceType;
            _itemDescription = ItemDescription;
            _nettAmount = NettAmount;
            _quantity = Quantity;
            _totalNettAmount = TotalNettAmount;
            _taxRate = TaxRate;
            _totalTax = TotalTax;
            _totalAmount = TotalAmount;
        }

        public BaseReceiptItem(Guid ViewedByUserId, SqlDataReader Reader) : base(ViewedByUserId, Reader)
		{
            string fieldname = "ReceiptItemId";
            _receiptItemId = Reader.GetGuid(Reader.GetOrdinal(fieldname));

            fieldname = "PaymentId";
            _paymentId = Reader.GetGuid(Reader.GetOrdinal(fieldname));

            fieldname = "PaymentReferenceId";
            _paymentReferenceId = Reader.GetGuid(Reader.GetOrdinal(fieldname));

            fieldname = "PaymentReferenceTypeId";
            if (!Reader.IsDBNull(Reader.GetOrdinal(fieldname))) _paymentReferenceType = (EnumPaymentReferenceType)Reader.GetInt32(Reader.GetOrdinal(fieldname));

            fieldname = "ItemDescription";
            _itemDescription = Reader.GetString(Reader.GetOrdinal(fieldname));

            fieldname = "NettAmount";
            _nettAmount = Convert.ToDouble(Reader.GetDecimal(Reader.GetOrdinal(fieldname)));

            fieldname = "Quantity";
            _quantity = Convert.ToDouble(Reader.GetDecimal(Reader.GetOrdinal(fieldname)));

            fieldname = "TotalNettAmount";
            _totalNettAmount = Convert.ToDouble(Reader.GetDecimal(Reader.GetOrdinal(fieldname)));

            fieldname = "TaxRate";
            _taxRate = Convert.ToDouble(Reader.GetDecimal(Reader.GetOrdinal(fieldname)));

            fieldname = "TotalTax";
            _totalTax = Convert.ToDouble(Reader.GetDecimal(Reader.GetOrdinal(fieldname)));

            fieldname = "TotalAmount";
            _totalAmount = Convert.ToDouble(Reader.GetDecimal(Reader.GetOrdinal(fieldname)));

            fieldname = "IsCredited";
            _isCredited = Reader.GetBoolean(Reader.GetOrdinal(fieldname));
        }
    }
}