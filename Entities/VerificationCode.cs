using System;
using System.Data.SqlClient;

using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities.Base;

namespace LittleLogBook.Data.Entities
{
	public class VerificationCode : BaseVerificationCode, IVerificationCode
	{
		public string VerificationCodeValueUrlEncoded
		{
			get
			{
				return Uri.EscapeDataString(base.VerificationCodeValue);
			}
		}

		public VerificationCode(Guid CreatedByUserId) : base(CreatedByUserId)
		{

		}

		public VerificationCode(Guid ViewedByUserId, SqlDataReader Reader) : base(ViewedByUserId, Reader)
		{

		}

		public VerificationCode(Guid CreatedByUserId, string MnemonicToken, string VerificationCodeValue,
			EnumVerificationType VerificationType, EnumVerificationCodeType VerificationCodeType, DateTime ExpirationDate)
			: base(CreatedByUserId, MnemonicToken, VerificationCodeValue, VerificationType, VerificationCodeType, ExpirationDate)
		{

		}
	}
}