using System;
using System.Data;
using System.Data.SqlClient;

namespace LittleLogBook.Data.Entities.Base.Cloud
{
	public class BaseCloudUser : BaseEntity
	{
		private Guid _cloudUserId = Guid.NewGuid();
		private EnumTitle _title = EnumTitle.Unknown;
		private string _firstName = null;
		private string _lastName = null;
		private string _emailAddress = null;
		private string _contactNumber = null;
		private string _preferredCurrencyIso = null;
		private EnumCloudUserStatus _cloudUserStatus = EnumCloudUserStatus.Unknown;

		public Guid CloudUserId
		{
			get
			{
				return _cloudUserId;
			}
		}

		[TemplateProperty]
		public EnumTitle Title
		{
			get
			{
				return _title;
			}
			set
			{
				if (_title != value)
				{
					_title = value;

					base.IsDirty = true;
				}
			}
		}

		[TemplateProperty]
		public string FirstName
		{
			get
			{
				return _firstName;
			}
			set
			{
				if (_firstName != value)
				{
					_firstName = value;

					base.IsDirty = true;
				}
			}
		}

		[TemplateProperty]
		public string LastName
		{
			get
			{
				return _lastName;
			}
			set
			{
				if (_lastName != value)
				{
					_lastName = value;

					base.IsDirty = true;
				}
			}
		}

		[TemplateProperty]
		public string EmailAddress
		{
			get
			{
				return _emailAddress;
			}
			set
			{
				if (_emailAddress != value)
				{
					_emailAddress = value;

					base.IsDirty = true;
				}
			}
		}

		[TemplateProperty]
		public string ContactNumber
		{
			get
			{
				return _contactNumber;
			}
			set
			{
				if (_contactNumber != value)
				{
					_contactNumber = value;

					base.IsDirty = true;
				}
			}
		}

		[TemplateProperty]
		public string PreferredCurrencyIso
		{
			get
			{
				return _preferredCurrencyIso ?? "ZAR";
			}
			set
			{
				if (_preferredCurrencyIso != value)
				{
					_preferredCurrencyIso = value;

					base.IsDirty = true;
				}
			}
		}

		public EnumCloudUserStatus CloudUserStatus
		{
			get
			{
				return _cloudUserStatus;
			}
			set
			{
				if (_cloudUserStatus != value)
				{
					_cloudUserStatus = value;

					base.IsDirty = true;
				}
			}
		}

		public BaseCloudUser(Guid CreatedByUserId, EnumTitle Title, string FirstName, string LastName, string EmailAddress, EnumCloudUserStatus CloudUserStatus, string PreferredCurrencyIso) : base(CreatedByUserId)
		{
			_title = Title;
			_firstName = FirstName;
			_lastName = LastName;
			_emailAddress = EmailAddress;
			_cloudUserStatus = CloudUserStatus;
			_preferredCurrencyIso = PreferredCurrencyIso;
		}

		public BaseCloudUser(Guid CreatedByUserId) : base(CreatedByUserId)
		{

		}

		public BaseCloudUser(Guid ViewedByUserId, SqlDataReader Reader) : base(ViewedByUserId, Reader)
		{
			string fieldname = null;

			fieldname = "CloudUserId";
			_cloudUserId = Reader.GetGuid(Reader.GetOrdinal(fieldname));

			fieldname = "TitleId";
			_title = (EnumTitle)Reader.GetInt32(Reader.GetOrdinal(fieldname));

			fieldname = "FirstName";
			_firstName = Reader.GetString(Reader.GetOrdinal(fieldname));

			fieldname = "LastName";
			_lastName = Reader.GetString(Reader.GetOrdinal(fieldname));
			
			fieldname = "ContactNumber";
			if (!Reader.IsDBNull(Reader.GetOrdinal(fieldname)))
			{
				_contactNumber = Reader.GetString(Reader.GetOrdinal(fieldname));
			}

			fieldname = "EmailAddress";
			_emailAddress = Reader.GetString(Reader.GetOrdinal(fieldname));

			fieldname = "CloudUserStatusId";
			_cloudUserStatus = (EnumCloudUserStatus) Reader.GetInt32(Reader.GetOrdinal(fieldname));

			fieldname = "PreferredCurrencyIso";
			if (Reader.IsDBNull(Reader.GetOrdinal(fieldname)))
			{
				_preferredCurrencyIso = "ZAR";
			}
			else
			{
				_preferredCurrencyIso = Reader.GetString(Reader.GetOrdinal(fieldname));
			}
		}
	}
}