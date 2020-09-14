using System;
using System.Data.SqlClient;

namespace LittleLogBook.Data.Entities.Base
{
    public class BaseUserPurseTransaction : BaseEntity
    {
        private Guid _userPurseTransactionId;
        private Guid _userPurseId;
        private double _transactionCredits;
        private string _transactionDescription;

        public Guid UserPurseTransactionId
        {
            get
            {
                return _userPurseTransactionId;
            }
        }

        public Guid UserPurseId
        {
            get
            {
                return _userPurseId;
            }
        }

        public double TransactionCredits
        {
            get
            {
                return _transactionCredits;
            }
        }

        public string TransactionDescription
        {
            get
            {
                return _transactionDescription;
            }
        }

        public BaseUserPurseTransaction(Guid ViewedByUserId, SqlDataReader Reader) : base(ViewedByUserId, Reader)
        {
            string fieldname = "UserPurseId";
            _userPurseId = Reader.GetGuid(Reader.GetOrdinal(fieldname));

            fieldname = "UserPurseTransactionId";
            _userPurseTransactionId = Reader.GetGuid(Reader.GetOrdinal(fieldname));

            fieldname = "TransactionCredits";
            _transactionCredits = Convert.ToDouble(Reader.GetDecimal(Reader.GetOrdinal(fieldname)));

            fieldname = "TransactionDescription";
            _transactionDescription = Reader.GetString(Reader.GetOrdinal(fieldname));
        }
    }
}