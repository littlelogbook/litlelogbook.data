using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

using LittleLogBook.Data.Common;
using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities;

namespace LittleLogBook.Data.Managers
{
    public class UserPurseManager : IUserPurseManager
    {
        private readonly IDataHandler _dataHandler;
        private readonly ICloudUser _currentUser;

        public UserPurseManager(IDataHandler dataHandler, ICloudUser currentUser)
        {
            _dataHandler = dataHandler;
            _currentUser = currentUser;
        }

        public async Task<IUserPurse> GetUserPurseAsync(Guid userId)
        {
            using (var command = _dataHandler.CreateCommand("GetUserPurse"))
            {
                command
                    .AddParameter("@UserId", userId, DbType.Guid)
                    .AddParameter("@ViewedByUserId", _currentUser.CloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new UserPurse(_currentUser.CloudUserId, reader);
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
                    .AddParameter("@ViewedByUserId", _currentUser.CloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        returnValues.Add(new UserPurseTransaction(_currentUser.CloudUserId, reader));
                    }
                }

                return returnValues;
            }
        }
    }
}
