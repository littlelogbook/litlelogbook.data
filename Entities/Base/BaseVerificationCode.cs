using System;
using System.Data;
using System.Data.SqlClient;

namespace LittleLogBook.Data.Entities.Base
{
    public class BaseVerificationCode : BaseEntity
    {
        private Guid _verificationCodeId = Guid.NewGuid();
        private string _mnemonicToken = null;
        private string _verificationCodeValue = null;
        private EnumVerificationType _verificationType = EnumVerificationType.Unknown;
        private EnumVerificationCodeType _verificationCodeType = EnumVerificationCodeType.Unknown;
        private DateTime _expirationDate = DateTime.MinValue;

        public Guid VerificationCodeId
        {
            get
            {
                return _verificationCodeId;
            }
        }

        public string MnemonicToken
        {
            get
            {
                return _mnemonicToken;
            }
            set
            {
                if (_mnemonicToken != value)
                {
                    _mnemonicToken = value;

                    base.IsDirty = true;
                }
            }
        }

        public string VerificationCodeValue
        {
            get
            {
                return _verificationCodeValue;
            }
            set
            {
                if (_verificationCodeValue != value)
                {
                    _verificationCodeValue = value;

                    base.IsDirty = true;
                }
            }
        }

        public EnumVerificationType VerificationType
        {
            get
            {
                return _verificationType;
            }
            set
            {
                if (_verificationType != value)
                {
                    _verificationType = value;

                    base.IsDirty = true;
                }
            }
        }

        public EnumVerificationCodeType VerificationCodeType
        {
            get
            {
                return _verificationCodeType;
            }
            set
            {
                if (_verificationCodeType != value)
                {
                    _verificationCodeType = value;

                    base.IsDirty = true;
                }
            }
        }

        public DateTime ExpirationDate
        {
            get
            {
                return _expirationDate;
            }
            set
            {
                if (_expirationDate != value)
                {
                    _expirationDate = value;

                    base.IsDirty = true;
                }
            }
        }

        public BaseVerificationCode(Guid CreatedByUserId, string MnemonicToken, string VerificationCodeValue, EnumVerificationType VerificationType, EnumVerificationCodeType VerificationCodeType, DateTime ExpirationDate) : base(CreatedByUserId)
        {
            _mnemonicToken = MnemonicToken;
            _verificationCodeValue = VerificationCodeValue;
            _verificationType = VerificationType;
            _verificationCodeType = VerificationCodeType;
            _expirationDate = ExpirationDate;
        }

        public BaseVerificationCode(Guid CreatedByUserId) : base(CreatedByUserId)
        {

        }

        public BaseVerificationCode(Guid ViewedByUserId, SqlDataReader Reader) : base(ViewedByUserId, Reader)
        {
            string fieldname = null;

            fieldname = "VerificationCodeId";
            _verificationCodeId = Reader.GetGuid(Reader.GetOrdinal(fieldname));

            fieldname = "MnemonicToken";
            _mnemonicToken = Reader.GetString(Reader.GetOrdinal(fieldname));

            fieldname = "VerificationCodeValue";
            _verificationCodeValue = Reader.GetString(Reader.GetOrdinal(fieldname));

            fieldname = "VerificationTypeId";
            _verificationType = (EnumVerificationType) Reader.GetInt32(Reader.GetOrdinal(fieldname));

            fieldname = "VerificationCodeTypeId";
            _verificationCodeType = (EnumVerificationCodeType) Reader.GetInt32(Reader.GetOrdinal(fieldname));

            fieldname = "ExpirationDate";
            _expirationDate = Reader.GetDateTime(Reader.GetOrdinal(fieldname));
        }
    }
}