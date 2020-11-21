using System;
using System.Data.SqlClient;

namespace LittleLogBook.Data.Entities.Base.Cloud
{
	public class BaseCloudUser : BaseEntity
	{
		private EnumTitle _title;
		private string _firstName;
		private string _lastName;
		private string _emailAddress;
		private string _contactNumber;
		private string _preferredCurrencyIso;
		private EnumCloudUserStatus _cloudUserStatus;
		private string _timezoneId;
		private string _memorableWord;
		private string _securityQuestion1;
		private string _securityAnswer1;
		private string _securityQuestion2;
		private string _securityAnswer2;
		private string _securityQuestion3;
		private string _securityAnswer3;
		
		public Guid CloudUserId { get; } = Guid.NewGuid();

		[TemplateProperty]
		public EnumTitle Title
		{
			get => _title;
			set
			{
				if (_title == value) return;
				_title = value;

				IsDirty = true;
			}
		}

		[TemplateProperty]
		public string FirstName
		{
			get => _firstName;
			set
			{
				if (_firstName == value) return;
				_firstName = value;

				IsDirty = true;
			}
		}

		[TemplateProperty]
		public string LastName
		{
			get => _lastName;
			set
			{
				if (_lastName == value) return;
				_lastName = value;

				IsDirty = true;
			}
		}

		[TemplateProperty]
		public string EmailAddress
		{
			get => _emailAddress;
			set
			{
				if (_emailAddress == value) return;
				_emailAddress = value;

				IsDirty = true;
			}
		}

		[TemplateProperty]
		public string ContactNumber
		{
			get => _contactNumber;
			set
			{
				if (_contactNumber == value) return;
				_contactNumber = value;

				IsDirty = true;
			}
		}

		[TemplateProperty]
		public string PreferredCurrencyIso
		{
			get => _preferredCurrencyIso ?? "ZAR";
			set
			{
				if (_preferredCurrencyIso == value) return;
				_preferredCurrencyIso = value;

				IsDirty = true;
			}
		}
		
		public EnumCloudUserStatus CloudUserStatus
		{
			get => _cloudUserStatus;
			set
			{
				if (_cloudUserStatus == value) return;
				_cloudUserStatus = value;

				IsDirty = true;
			}
		}

		public string TimezoneId
		{
			get => _timezoneId ?? "ZAR";
			set
			{
				if (_timezoneId == value) return;
				
				_timezoneId = value;

				IsDirty = true;
			}
		}
		
		public string MemorableWord
		{
			get => _memorableWord;
			set
			{
				if (_memorableWord == value) return;
				_memorableWord = value;

				IsDirty = true;
			}
		}

		public string SecurityQuestion1
		{
			get => _securityQuestion1;
			set
			{
				if (_securityQuestion1 == value) return;
				_securityQuestion1 = value;

				IsDirty = true;
			}
		}

		public string SecurityAnswer1
		{
			get => _securityAnswer1;
			set
			{
				if (_securityAnswer1 == value) return;
				_securityAnswer1 = value;

				IsDirty = true;
			}
		}

		public string SecurityQuestion2
		{
			get => _securityQuestion2;
			set
			{
				if (_securityQuestion2 == value) return;
				_securityQuestion2 = value;

				IsDirty = true;
			}
		}

		public string SecurityAnswer2
		{
			get => _securityAnswer2;
			set
			{
				if (_securityAnswer2 == value) return;
				_securityAnswer2 = value;

				IsDirty = true;
			}
		}

		public string SecurityQuestion3
		{
			get => _securityQuestion3;
			set
			{
				if (_securityQuestion3 == value) return;
				_securityQuestion3 = value;

				IsDirty = true;
			}
		}

		public string SecurityAnswer3
		{
			get => _securityAnswer3;
			set
			{
				if (_securityAnswer3 == value) return;
				_securityAnswer3 = value;

				IsDirty = true;
			}
		}
			
		protected BaseCloudUser(Guid CreatedByUserId, EnumTitle Title, string FirstName, string LastName, string EmailAddress, 
			EnumCloudUserStatus CloudUserStatus, string PreferredCurrencyIso, string TimezoneId, string MemorableWord,
			string SecurityQuestion1, string SecurityAnswer1, string SecurityQuestion2, string SecurityAnswer2, string SecurityQuestion3,
			string SecurityAnswer3)
			: base(CreatedByUserId)
		{
			_title = Title;
			_firstName = FirstName;
			_lastName = LastName;
			_emailAddress = EmailAddress;
			_cloudUserStatus = CloudUserStatus;
			_preferredCurrencyIso = PreferredCurrencyIso;
			_timezoneId = TimezoneId;
			_memorableWord = MemorableWord;
			_securityQuestion1 = SecurityQuestion1;
			_securityAnswer1 = SecurityAnswer1;
			_securityQuestion2 = SecurityQuestion2;
			_securityAnswer2 = SecurityAnswer2;
			_securityQuestion3 = SecurityQuestion3;
			_securityAnswer3 = SecurityAnswer3;
		}

		protected BaseCloudUser(Guid CreatedByUserId) : base(CreatedByUserId)
		{

		}

		protected BaseCloudUser(Guid ViewedByUserId, SqlDataReader Reader) : base(ViewedByUserId, Reader)
		{
			var fieldName = "CloudUserId";
			CloudUserId = Reader.GetGuid(Reader.GetOrdinal(fieldName));

			fieldName = "TitleId";
			_title = (EnumTitle)Reader.GetInt32(Reader.GetOrdinal(fieldName));

			fieldName = "FirstName";
			_firstName = Reader.GetString(Reader.GetOrdinal(fieldName));

			fieldName = "LastName";
			_lastName = Reader.GetString(Reader.GetOrdinal(fieldName));
			
			fieldName = "ContactNumber";
			if (!Reader.IsDBNull(Reader.GetOrdinal(fieldName)))
			{
				_contactNumber = Reader.GetString(Reader.GetOrdinal(fieldName));
			}

			fieldName = "EmailAddress";
			_emailAddress = Reader.GetString(Reader.GetOrdinal(fieldName));

			fieldName = "CloudUserStatusId";
			_cloudUserStatus = (EnumCloudUserStatus) Reader.GetInt32(Reader.GetOrdinal(fieldName));

			fieldName = "PreferredCurrencyIso";
			_preferredCurrencyIso = Reader.IsDBNull(Reader.GetOrdinal(fieldName)) ? "ZAR" : Reader.GetString(Reader.GetOrdinal(fieldName));

			fieldName = "TimezoneId";
			_timezoneId = Reader.IsDBNull(Reader.GetOrdinal(fieldName)) ? "Africa/Johannesburg" : Reader.GetString(Reader.GetOrdinal(fieldName));

			fieldName = "MemorableWord";
			_memorableWord = Reader.GetString(Reader.GetOrdinal(fieldName));

			fieldName = "SecurityQuestion1";
			_securityQuestion1 = Reader.GetString(Reader.GetOrdinal(fieldName));
			
			fieldName = "SecurityAnswer1";
			_securityAnswer1 = Reader.GetString(Reader.GetOrdinal(fieldName));

			fieldName = "SecurityQuestion2";
			_securityQuestion2 = Reader.GetString(Reader.GetOrdinal(fieldName));
			
			fieldName = "SecurityAnswer2";
			_securityAnswer2 = Reader.GetString(Reader.GetOrdinal(fieldName));

			fieldName = "SecurityQuestion3";
			_securityQuestion3 = Reader.GetString(Reader.GetOrdinal(fieldName));
			
			fieldName = "SecurityAnswer3";
			_securityAnswer3 = Reader.GetString(Reader.GetOrdinal(fieldName));
		}
	}
}