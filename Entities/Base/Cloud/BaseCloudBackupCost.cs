using System;
using System.Data;

namespace LittleLogBook.Data.Entities.Base.Cloud
{
    public class BaseCloudBackupCost
    {
        public Guid CloudUserId { get; }

        public int CreditCost { get; }

        public long DataLength { get; }

        public double PurseBalance { get; }

        public BaseCloudBackupCost(IDataReader Reader)
        {
            string fieldname = null;

            fieldname = "CloudUserId";
            this.CloudUserId = Reader.GetGuid(Reader.GetOrdinal(fieldname));

            fieldname = "CreditCost";
            this.CreditCost = Reader.GetInt32(Reader.GetOrdinal(fieldname));

            fieldname = "DataLength";
            this.DataLength = Reader.GetInt64(Reader.GetOrdinal(fieldname));

            fieldname = "PurseBalance";
            this.PurseBalance = Convert.ToDouble(Reader.GetDecimal(Reader.GetOrdinal(fieldname)));
        }
    }
}