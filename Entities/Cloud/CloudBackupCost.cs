using System.Data;

using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities.Base.Cloud;

namespace LittleLogBook.Data.Entities.Cloud
{
    public class CloudBackupCost : BaseCloudBackupCost, IBackupCost
	{
		public CloudBackupCost(IDataReader Reader) : base(Reader)
		{

		}
    }
}