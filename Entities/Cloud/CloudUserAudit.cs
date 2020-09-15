using System.Data;

using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities.Base.Cloud;

namespace LittleLogBook.Data.Entities.Cloud
{
    public class CloudUserAudit : BaseCloudUserAudit, IUserAudit
	{
		public CloudUserAudit(IDataReader reader) : base(reader)
		{

		}
    }
}