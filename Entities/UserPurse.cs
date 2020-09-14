using System;
using System.Data.SqlClient;

using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities.Base;

namespace LittleLogBook.Data.Entities
{
	public class UserPurse : BaseUserPurse, IUserPurse
	{
		public UserPurse(Guid ViewedByUserId, SqlDataReader Reader) : base(ViewedByUserId, Reader)
		{

		}
	}
}