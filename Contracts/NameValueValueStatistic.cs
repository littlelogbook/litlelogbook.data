using System;
using System.Data;

namespace LittleLogBook.Data.Contracts
{
    public class NameValueValueStatistic : NameValueStatistic, INameValueValueStatistic
    {
        public int ItemValue2 { get; set; }
        
        public NameValueValueStatistic(Guid viewedByUserId, IDataReader reader)
            : base(viewedByUserId, reader)
        {
            var fieldName = "ItemValue2";
            ItemValue2 = reader.GetInt32(reader.GetOrdinal(fieldName));
        }
    }
}