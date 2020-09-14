using System;
using System.Data;

using LittleLogBook.Data.Contracts;

namespace LittleLogBook.Data.Entities.Cloud
{
    public class Statistic : IStatistic
    {
        public string ItemName { get; set; }
        public string ItemLabel { get; set; }
        public string ItemValue { get; set; }
        public Guid ViewedByUserId { get; }

        public Statistic(Guid ViewedByUserId, IDataReader Reader)
        {
            string fieldname = "ItemName";
            ItemName = Reader.GetString(Reader.GetOrdinal(fieldname));

            fieldname = "ItemLabel";
            ItemLabel = Reader.GetString(Reader.GetOrdinal(fieldname));

            fieldname = "ItemValue";
            ItemValue = Reader.GetString(Reader.GetOrdinal(fieldname));

            this.ViewedByUserId = ViewedByUserId;
        }
    }
}