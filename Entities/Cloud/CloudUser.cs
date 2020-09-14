using System;
using System.Data.SqlClient;

using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities.Base.Cloud;

namespace LittleLogBook.Data.Entities.Cloud
{
    public class CloudUser : BaseCloudUser, ICloudUser
	{
		public string EmailAddressUrlEncoded
		{
			get
			{
				return Uri.EscapeDataString(base.EmailAddress);
			}
		}

		public CloudUser(Guid CreatedByUserId) : base(CreatedByUserId)
		{

		}

		public CloudUser(Guid ViewedByUserId, SqlDataReader Reader) : base(ViewedByUserId, Reader)
		{

		}

		public CloudUser(Guid CreatedByUserId, EnumTitle Title, string FirstName, string LastName,
			string EmailAddress, EnumCloudUserStatus CloudUserStatus, string PreferredCurrencyIso)
			: base(CreatedByUserId, Title, FirstName, LastName, EmailAddress, CloudUserStatus, PreferredCurrencyIso)
		{

		}

        public override string ToString()
        {
            string returnValue = ((base.Title + " " + base.FirstName).Trim() + " " + base.LastName).Trim();

            return returnValue;
        }
    }
}