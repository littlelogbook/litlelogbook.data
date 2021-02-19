using System;
using System.Data;

namespace LittleLogBook.Data.Contracts
{
    public class NameValueStatistic : INameValueStatistic
    {
        public Guid ViewedByUserId { get; }
        
        public string ItemName { get; set; }
        
        public int ItemValue { get; set; }
        
        public NameValueStatistic(Guid ViewedByUserId, IDataReader Reader)
        {
            var fieldName = "ItemName";
            ItemName = Reader.GetString(Reader.GetOrdinal(fieldName));

            fieldName = "ItemValue";
            ItemValue = Reader.GetInt32(Reader.GetOrdinal(fieldName));

            this.ViewedByUserId = ViewedByUserId;
        }
    }
}