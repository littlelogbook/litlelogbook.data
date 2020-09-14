using System;
using System.Data;
using System.Data.SqlClient;

namespace LittleLogBook.Data.Entities.Base
{
    public class BaseUserPurse : BaseEntity
    {
        private Guid _userPurseId;
        private Guid _userId;
        private EnumPurseType _purseType;
        private string _purseName;
        private string _purseDescription;
        private double _balance;
        private EnumPurseStatus _purseStatus;

        public Guid UserPurseId
        {
            get
            {
                return _userPurseId;
            }
        }

        public Guid UserId
        {
            get
            {
                return _userId;
            }
        }

        public EnumPurseType PurseType
        {
            get
            {
                return _purseType;
            }
        }

        public string PurseName
        {
            get
            {
                return _purseName;
            }
        }

        public string PurseDescription
        {
            get
            {
                return _purseDescription;
            }
        }

        public double Balance
        {
            get
            {
                return _balance;
            }
        }

        public EnumPurseStatus PurseStatus
        {
            get
            {
                return _purseStatus;
            }
        }

        public BaseUserPurse(Guid ViewedByUserId, SqlDataReader Reader) : base(ViewedByUserId, Reader)
        {
            string fieldname = "UserPurseId";
            _userPurseId = Reader.GetGuid(Reader.GetOrdinal(fieldname));

            fieldname = "UserId";
            _userId = Reader.GetGuid(Reader.GetOrdinal(fieldname));

            fieldname = "PurseTypeId";
            _purseType = (EnumPurseType) Reader.GetInt32(Reader.GetOrdinal(fieldname));

            fieldname = "PurseName";
            if (!Reader.IsDBNull(Reader.GetOrdinal(fieldname)))
            {
                _purseName = Reader.GetString(Reader.GetOrdinal(fieldname));
            }

            fieldname = "PurseDescription";
            if (!Reader.IsDBNull(Reader.GetOrdinal(fieldname)))
            {
                _purseDescription = Reader.GetString(Reader.GetOrdinal(fieldname));
            }

            fieldname = "Balance";
            _balance = Convert.ToDouble(Reader.GetDecimal(Reader.GetOrdinal(fieldname)));

            fieldname = "PurseStatusId";
            _purseStatus = (EnumPurseStatus) Reader.GetInt32(Reader.GetOrdinal(fieldname));
        }
    }
}