using System;
using System.Data;
using System.Data.SqlClient;

namespace LittleLogBook.Data.Entities.Base
{
    public class BaseCurrency : BaseEntity
    {
        private string _currencyId = null;
        private string _symbol = null;
        private string _currencyName = null;
        private double _exchangeRate = 0;
        private bool _isActive = false;

        public string CurrencyId
        {
            get
            {
                return _currencyId;
            }
        }

        public string Symbol
        {
            get
            {
                return _symbol;
            }
            set
            {
                if (_symbol != value)
                {
                    _symbol = value;

                    base.IsDirty = true;
                }
            }
        }

        public string CurrencyName
        {
            get
            {
                return _currencyName;
            }
            set
            {
                if (_currencyName != value)
                {
                    _currencyName = value;

                    base.IsDirty = true;
                }
            }
        }

        public double ExchangeRate
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

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;

                    base.IsDirty = true;
                }
            }
        }

        public BaseCurrency(Guid CreatedByUserId, string CurrencyId, string Symbol, string CurrencyName, bool IsActive) : base(CreatedByUserId)
        {
            _currencyId = CurrencyId;
            _symbol = Symbol;
            _currencyName = CurrencyName;
            _isActive = IsActive;
        }

        public BaseCurrency(Guid ViewedByUserId, SqlDataReader Reader) : base(ViewedByUserId, Reader)
        {
            string fieldname = null;

            fieldname = "CurrencyId";
            _currencyId = Reader.GetString(Reader.GetOrdinal(fieldname));

            fieldname = "Symbol";
            _symbol = Reader.GetString(Reader.GetOrdinal(fieldname));

            fieldname = "CurrencyName";
            _currencyName = Reader.GetString(Reader.GetOrdinal(fieldname));

            fieldname = "ExchangeRate";
            if (!Reader.IsDBNull(Reader.GetOrdinal(fieldname)))
            {
                _exchangeRate = Convert.ToDouble(Reader.GetDecimal(Reader.GetOrdinal(fieldname)));
            }

            fieldname = "IsActive";
            _isActive = Reader.GetBoolean(Reader.GetOrdinal(fieldname));
        }
    }
}