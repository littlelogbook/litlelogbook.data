using System;
using System.Data;

namespace LittleLogBook.Data.Entities.Base.Cloud
{
	public class BaseCloudUserAudit
	{
		public Guid CloudUserAuditId { get; }

		public Guid CloudUserId { get; }

		public string AuditDescription { get; }

		public DateTime DateCreated { get; }

		public BaseCloudUserAudit(IDataReader reader)
		{
			string fieldname = null;

			fieldname = "CloudUserAuditId";
			this.CloudUserAuditId = reader.GetGuid(reader.GetOrdinal(fieldname));

			fieldname = "CloudUserId";
			this.CloudUserAuditId = reader.GetGuid(reader.GetOrdinal(fieldname));

			fieldname = "AuditDescription";
			this.AuditDescription = reader.GetString(reader.GetOrdinal(fieldname));

			fieldname = "DateCreated";
			this.DateCreated = reader.GetDateTime(reader.GetOrdinal(fieldname));
		}
    }
}