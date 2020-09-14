using System;
using System.Data.SqlClient;

using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities.Base;

namespace LittleLogBook.Data.Entities
{
    public class UserPurseTransaction : BaseUserPurseTransaction, IUserPurseTransaction
	{
		public UserPurseTransaction(Guid ViewedByUserId, SqlDataReader Reader) : base(ViewedByUserId, Reader)
		{

		}
	}
}