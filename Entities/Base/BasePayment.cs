using System;
using System.Data.SqlClient;

namespace LittleLogBook.Data.Entities.Base
{
	public class BasePayment : BaseEntity
	{
		private Guid _paymentId = Guid.NewGuid();
		private Guid _userId;
		private EnumPaymentGatewayProvider _paymentGatewayProvider;
		private DateTime _dateTimeRequested = DateTime.UtcNow;
		private DateTime _dateTimeResponded;
		private string _reference;
        private string _paymentStatusMessage;
        private EnumPaymentStatus _paymentStatus;
        private Guid _paymentReferenceId;
        private EnumPaymentReferenceType _paymentReferenceType;
        private string _currencyId;
		private double _exchangeRate;
		private double _amount;

        public Guid PaymentId
		{
			get
			{
				return _paymentId;
			}
		}

		public Guid UserId
		{
			get
			{
				return _userId;
			}
		}

		public EnumPaymentGatewayProvider PaymentGatewayProvider
		{
			get
			{
				return _paymentGatewayProvider;
			}
		}

		public DateTime DateTimeRequested
		{
			get
			{
				return _dateTimeRequested;
			}
		}

		public DateTime DateTimeResponded
		{
			get
			{
				return _dateTimeResponded;
			}
			set
			{
				if (_dateTimeResponded != value)
				{
					_dateTimeResponded = value;

					base.IsDirty = true;
				}
			}
		}

        public string PaymentStatusMessage
        {
            get
            {
                return _paymentStatusMessage;
            }
            set
            {
                if (_paymentStatusMessage != value)
                {
                    _paymentStatusMessage = value;

                    base.IsDirty = true;
                }
            }
        }

        public EnumPaymentStatus PaymentStatus
        {
            get
            {
                return _paymentStatus;
            }
            set
            {
                if (_paymentStatus != value)
                {
                    _paymentStatus = value;

                    base.IsDirty = true;
                }
            }
        }

		public int CustomerPaymentReferenceNumber { get; internal set; }

		public string Reference
		{
			get
			{
				return _reference;
			}
			set
			{
				if (_reference != value)
				{
					_reference = value;

					base.IsDirty = true;
				}
			}
		}

        public Guid PaymentReferenceId
        {
            get
            {
                return _paymentReferenceId;
            }
            set
            {
                if (_paymentReferenceId != value)
                {
                    _paymentReferenceId = value;

                    base.IsDirty = true;
                }
            }
        }

        public EnumPaymentReferenceType PaymentReferenceType
        {
            get
            {
                return _paymentReferenceType;
            }
            set
            {
                if (_paymentReferenceType != value)
                {
                    _paymentReferenceType = value;

                    base.IsDirty = true;
                }
            }
        }

        public string CurrencyId
		{
			get
			{
				return _currencyId;
			}
			set
			{
				if (_currencyId != value)
				{
					_currencyId = value;

					base.IsDirty = true;
				}
			}
		}

		private double ExchangeRate
		{
			get
			{
				return _exchangeRate;
			}
			set
			{
				if (_exchangeRate != value)
				{
					_exchangeRate = value;

					base.IsDirty = true;
				}
			}
		}

        public double Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                if (_amount != value)
                {
                    _amount = value;

                    base.IsDirty = true;
                }
            }
        }

		public BasePayment(Guid CreatedByUserId, Guid UserId, EnumPaymentGatewayProvider PaymentGatewayProvider, string Reference, Guid PaymentReferenceId, EnumPaymentReferenceType PaymentReferenceType, string CurrencyId, double ExchangeRate, double Amount) : base(CreatedByUserId)
		{
			_userId = UserId;
			_paymentGatewayProvider = PaymentGatewayProvider;
			_reference = Reference;
            _paymentReferenceId = PaymentReferenceId;
            _paymentReferenceType = PaymentReferenceType;
            _currencyId = CurrencyId;
			_exchangeRate = ExchangeRate;
			_amount = Amount;
        }

		public BasePayment(Guid ViewedByUserId, SqlDataReader Reader) : base(ViewedByUserId, Reader)
		{
			string fieldname = null;

			fieldname = "PaymentId";
			_paymentId = Reader.GetGuid(Reader.GetOrdinal(fieldname));
			
			fieldname = "UserId";
			_userId = Reader.GetGuid(Reader.GetOrdinal(fieldname));
			
			fieldname = "PaymentGatewayProviderId";
			_paymentGatewayProvider = (EnumPaymentGatewayProvider)Reader.GetInt32(Reader.GetOrdinal(fieldname));
			
			fieldname = "DateTimeRequested";
			_dateTimeRequested = Reader.GetDateTime(Reader.GetOrdinal(fieldname));

			fieldname = "DateTimeResponded";
            if (!Reader.IsDBNull(Reader.GetOrdinal(fieldname)))
            {
                _dateTimeResponded = Reader.GetDateTime(Reader.GetOrdinal(fieldname));
            }

            fieldname = "Reference";
            if (!Reader.IsDBNull(Reader.GetOrdinal(fieldname)))
            {
                _reference = Reader.GetString(Reader.GetOrdinal(fieldname));
            }

            fieldname = "PaymentStatus";
            if (!Reader.IsDBNull(Reader.GetOrdinal(fieldname)))
            {
                _paymentStatusMessage = Reader.GetString(Reader.GetOrdinal(fieldname));
            }

            fieldname = "PaymentStatusId";
            _paymentStatus = (EnumPaymentStatus)Reader.GetInt32(Reader.GetOrdinal(fieldname));

            fieldname = "PaymentReferenceId";
            _paymentReferenceId = Reader.GetGuid(Reader.GetOrdinal(fieldname));

            fieldname = "PaymentReferenceTypeId";
            _paymentReferenceType = (EnumPaymentReferenceType)Reader.GetInt32(Reader.GetOrdinal(fieldname));

            fieldname = "CurrencyId";
			_currencyId = Reader.GetString(Reader.GetOrdinal(fieldname));

			fieldname = "ExchangeRate";
			_exchangeRate = Convert.ToDouble(Reader.GetDecimal(Reader.GetOrdinal(fieldname)));

			fieldname = "Amount";
			_amount = Convert.ToDouble(Reader.GetDecimal(Reader.GetOrdinal(fieldname)));

			fieldname = "CustomerPaymentReferenceNumber";
			CustomerPaymentReferenceNumber = Reader.GetInt32(Reader.GetOrdinal(fieldname));
		}
	}
}