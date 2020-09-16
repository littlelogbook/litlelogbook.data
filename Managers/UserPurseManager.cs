using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities;
using LittleLogBook.Data.SqlConnectivity;

namespace LittleLogBook.Data.Managers
{
    public class UserPurseManager : ManagerBase, IUserPurseManager
    {
        private readonly IDataHandler _dataHandler;

        internal UserPurseManager(IDataHandler dataHandler, IUser currentUser)
            : base(currentUser)
        {
            _dataHandler = dataHandler;
        }

        public async Task<IUserPurse> GetUserPurseAsync(Guid userId)
        {
            using (var command = _dataHandler.CreateCommand("GetUserPurse"))
            {
                command
                    .AddParameter("@UserId", userId, DbType.Guid)
                    .AddParameter("@ViewedByUserId", CurrentUser.CloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new UserPurse(CurrentUser.CloudUserId, reader);
                    }
                }

                return null;
            }
        }

        public async Task<IEnumerable<IUserPurseTransaction>> GetUserPurseTransactionsAsync(Guid userPurseId)
        {
            if (Guid.Empty.Equals(userPurseId))
            {
                throw new ArgumentNullException("The specified purse ID may not be empty");
            }

            var returnValues = new List<IUserPurseTransaction>();

            using (var command = _dataHandler.CreateCommand("GetUserPurseTransactions"))
            {
                command
                    .AddParameter("@UserPurseId", userPurseId, DbType.Guid)
                    .AddParameter("@ViewedByUserId", CurrentUser.CloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        returnValues.Add(new UserPurseTransaction(CurrentUser.CloudUserId, reader));
                    }
                }

                return returnValues;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dataHandler?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
