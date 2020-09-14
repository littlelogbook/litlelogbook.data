using System.Data;

namespace LittleLogBook.Data.Entities
{
    public class IpLocation
    {
        private double _ipFrom = 0;
        private double _ipTo = 0;
        private int _countryId = 0;

        public double IpFrom
        {
            get
            {
                return _ipFrom;
            }
        }

        public double IpTo
        {
            get
            {
                return _ipTo;
            }
        }

        public int CountryId
        {
            get
            {
                return _countryId;
            }
        }

        public IpLocation(IDataReader Reader)
        {
            string fieldname = null;

            fieldname = "IpFrom";
            _ipFrom = Reader.GetDouble(Reader.GetOrdinal(fieldname));

            fieldname = "IpTo";
            _ipTo = Reader.GetDouble(Reader.GetOrdinal(fieldname));

            fieldname = "CountryId";
            _countryId = Reader.GetInt32(Reader.GetOrdinal(fieldname));
        }
    }
}
