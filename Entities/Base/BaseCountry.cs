using System;
using System.Data;
using System.Data.SqlClient;
using LittleLogBook.Data.Contracts;

namespace LittleLogBook.Data.Entities.Base
{
    public class BaseCountry : BaseEntity, ICountry
    {
        private int _countryId = 0;
        private string _iso2 = null;
        private string _name = null;
        private string _iso3 = null;
        private int _countryCode = 0;
        private double _taxRate = 0;
        private bool _isActive = false;

        public int CountryId
        {
            get
            {
                return _countryId;
            }
        }

        public string Iso2
        {
            get
            {
                return _iso2;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Iso3
        {
            get
            {
                return _iso3;
            }
        }

        public int CountryCode
        {
            get
            {
                return _countryCode;
            }
        }

        public double TaxRate
        {
            get
            {
                return _taxRate;
            }
        }

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
        }

        public BaseCountry(Guid ViewedByUserId, SqlDataReader Reader) : base(ViewedByUserId, Reader)
        {
            string fieldname = "CountryId";
            _countryId = Reader.GetInt32(Reader.GetOrdinal(fieldname));

            fieldname = "Iso2";
            _iso2 = Reader.GetString(Reader.GetOrdinal(fieldname));

            fieldname = "Name";
            _name = Reader.GetString(Reader.GetOrdinal(fieldname));

            fieldname = "Iso3";
            _iso3 = Reader.GetString(Reader.GetOrdinal(fieldname));

            fieldname = "CountryCode";
            _countryCode = Reader.GetInt32(Reader.GetOrdinal(fieldname));

            fieldname = "TaxRate";
            _taxRate = Convert.ToDouble(Reader.GetDecimal(Reader.GetOrdinal(fieldname)));

            fieldname = "IsActive";
            _isActive = Reader.GetBoolean(Reader.GetOrdinal(fieldname));
        }
    }
}