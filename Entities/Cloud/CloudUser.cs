using System;
using System.Data.SqlClient;

using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities.Base.Cloud;

namespace LittleLogBook.Data.Entities.Cloud
{
    public class CloudUser : BaseCloudUser, IUser
	{
		public string EmailAddressUrlEncoded => Uri.EscapeDataString(EmailAddress);

		public CloudUser(Guid CreatedByUserId) : base(CreatedByUserId)
		{

		}

		public CloudUser() : base(Guid.Empty)
		{
			CreatedByUserId = CloudUserId;
		}

		public CloudUser(SqlDataReader Reader) : this(Reader.GetGuid(Reader.GetOrdinal("CloudUserId")), Reader)
		{

		}

		public CloudUser(Guid ViewedByUserId, SqlDataReader Reader) : base(ViewedByUserId, Reader)
		{

		}

		public CloudUser(Guid CreatedByUserId, EnumTitle Title, string FirstName, string LastName, string EmailAddress, EnumCloudUserStatus CloudUserStatus,
			string PreferredCurrencyIso, string PreferredTimeZoneId, string MemorableWord, string SecurityQuestion1, string SecurityAnswer1,
			string SecurityQuestion2, string SecurityAnswer2, string SecurityQuestion3,string SecurityAnswer3)
			: base(CreatedByUserId, Title, FirstName, LastName, EmailAddress, CloudUserStatus, PreferredCurrencyIso, PreferredTimeZoneId,
				MemorableWord, SecurityQuestion1, SecurityAnswer1, SecurityQuestion2, SecurityAnswer2, SecurityQuestion3, SecurityAnswer3)
		{

		}

        public override string ToString()
        {
            var returnValue = ((Title + " " + FirstName).Trim() + " " + LastName).Trim();

            return returnValue;
        }
    }
}