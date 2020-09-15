using System.Data;

using LittleLogBook.Data.Contracts;

namespace LittleLogBook.Data.Entities
{
    public class IpLocation : IIpLocation
    {
        public double IpFrom { get; }

        public double IpTo { get; }

        public int CountryId { get; }

        public IpLocation(IDataReader Reader)
        {
            string fieldname = "IpFrom";
            IpFrom = Reader.GetDouble(Reader.GetOrdinal(fieldname));

            fieldname = "IpTo";
            IpTo = Reader.GetDouble(Reader.GetOrdinal(fieldname));

            fieldname = "CountryId";
            CountryId = Reader.GetInt32(Reader.GetOrdinal(fieldname));
        }
    }
}
