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

		EnumCloudUserRole CloudUserRole { get; set; }

		string TimezoneId { get; set; }
		
		string MemorableWord { get; set; }
		
		string SecurityQuestion1 { get; set; }
		
		string SecurityAnswer1 { get; set; }
		
		string SecurityQuestion2 { get; set; }
		
		string SecurityAnswer2 { get; set; }
		
		string SecurityQuestion3 { get; set; }
		
		string SecurityAnswer3 { get; set; }
		
		string EmailAddressUrlEncoded { get; }

        Guid? ViewedByUserId { get; }

        Guid CreatedByUserId { get; }
	}
}