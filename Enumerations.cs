using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleLogBook.Data
{
	public enum EnumCloudUserStatus
	{
		Unknown = 0,
		Unverified = 1,
		Active = 2,
		Suspended = 3,
		Deactivated = 4
	}

	public enum EnumTitle
	{
		Unknown = 0,
		Mr = 1,
		Mrs = 2,
		Miss = 3,
		Dr = 4,
		Ms = 5,
		Prof = 6,
		Rev = 7
	}

	public enum EnumVerificationType
	{
		Unknown = 0,
		EmailAddress = 1,
		ForgotPassword = 2
	}

	public enum EnumVerificationCodeType
	{
		Unknown = 0,
		Complex = 1,
		Otp = 2
	}

	public enum EnumBackupEntryStatus
	{
		Unknown = 0,
		Current = 1,
		Completed = 2,
		Cancelled = 3
	}

	public enum EnumBackupEntryType
	{
		Unknown = 0,
		Backup = 1,
		Restore  =2
	}

	public enum EnumPaymentStatus
	{
		NotDone = 0,
		Approved = 1,
		Declined = 2,
		Cancelled = 3,
		UserCancelled = 4,
		ReceivedByProvider = 5,
		SettlementVoided = 6,
		InsufficientFunds = 7,
		InvalidCardNumber = 8,
		InvalidAmount = 9,
		InvalidExpiryDate = 10,
		ThreeDeeSecureTimeout = 11,
		AuthDeclined = 12,
		AuthFailed = 13,
		CardBlacklisted = 14,
		ExcessiveCardUsage = 15,
		RestrictedCard = 16,
		CardReportedAsStolen = 17,
		SuspectedFraud = 18,
		InvalidCardLength = 19,
		LostCard = 20,
		InvalidCard = 21,
		BankInterfaceTimeout = 22,
		CardExpired = 23,
		CallForApproval = 24
	}
}