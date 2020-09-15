using System;

namespace LittleLogBook.Data.Contracts
{
	public interface IUser
	{
		Guid CloudUserId { get; }

		EnumTitle Title { get; set; }

		string FirstName { get; set; }

		string LastName { get; set; }

		string EmailAddress { get; set; }

		string ContactNumber { get; set; }

		string PreferredCurrencyIso { get; set; }

		EnumCloudUserStatus CloudUserStatus { get; set; }

		string EmailAddressUrlEncoded { get; }

        Guid? ViewedByUserId { get; }

        Guid CreatedByUserId { get; }

        string ToString();
	}
}