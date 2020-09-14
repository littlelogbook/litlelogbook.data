using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

using LittleLogBook.Data.Common;
using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities.Cloud;

namespace LittleLogBook.Data.Managers
{
    public class StatisticsManager : IStatisticsManager
    {
        private readonly IDataHandler _dataHandler;
        private readonly ICloudUser _currentUser;

        public StatisticsManager(IDataHandler dataHandler, ICloudUser currentUser)
        {
            _dataHandler = dataHandler;
            _currentUser = currentUser;
        }

        public async Task<IEnumerable<IStatistic>> GetStatisticsForUserAsync(Guid cloudUserId)
        {
            if (Guid.Empty.Equals(cloudUserId))
            {
                throw new ArgumentNullException("The specified cloud user ID may not be empty");
            }

            var returnValues = new List<Statistic>();

            using (var command = _dataHandler.CreateCommand("GetStatisticsForUser"))
            {
                command
                    .AddParameter("@CloudUserId", cloudUserId, DbType.Guid)
                    .AddParameter("@ViewedByUserId", _currentUser.CloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        returnValues.Add(new Statistic(_currentUser.CloudUserId, reader));
                    }
                }

                return returnValues;
            }
        }

        public async Task<bool> UpdateBackupEntryAsync(IBackupEntry backupEntry)
        {
            using (var command = _dataHandler.CreateCommand("UpdateBackupEntry"))
            {
                command
                    .AddParameter("@BackupEntryId", backupEntry.BackupEntryId, DbType.Guid)
                    .AddParameter("@BackupName", backupEntry.BackupName, DbType.String)
                    .AddParameter("@BackupDescription", backupEntry.BackupDescription, DbType.String)
                    .AddParameter("@ModifiedByUserId", _currentUser.CloudUserId, DbType.Guid);

                if (await command.ExecuteNonQueryAsync() > 0)
                {
                    ((BackupEntry) backupEntry).SetInternals(false, false, DateTime.UtcNow, _currentUser.CloudUserId);

                    return true;
                }

                return false;
            }
        }

        public async Task<bool> FinishBackupEntryAsync(Guid backupEntryId, long backupSize)
        {
            using (var command = _dataHandler.CreateCommand("FinishBackupEntry"))
            {
                command
                    .AddParameter("@BackupEntryId", backupEntryId, DbType.Guid)
                    .AddParameter("@BackupSize", backupSize, DbType.Int64)
                    .AddParameter("@ModifiedByUserId", _currentUser.CloudUserId, DbType.Guid);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<bool> CancelBackupEntryAsync(Guid backupEntryId, long backupSize)
        {
            using (var command = _dataHandler.CreateCommand("CancelBackupEntry"))
            {
                command
                    .AddParameter("@BackupEntryId", backupEntryId, DbType.Guid)
                    .AddParameter("@BackupSize", backupSize, DbType.Int64)
                    .AddParameter("@ModifiedByUserId", _currentUser.CloudUserId, DbType.Guid);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<bool> InsertBackupEntryAsync(IBackupEntry backupEntry)
        {
            using (var command = _dataHandler.CreateCommand("InsertBackupEntry"))
            {
                command
                    .AddParameter("@BackupEntryId", backupEntry.BackupEntryId, DbType.Guid)
                    .AddParameter("@CloudUserId", backupEntry.CloudUserId, DbType.Guid)
                    .AddParameter("@BackupEntryTypeId", (int) backupEntry.BackupEntryType, DbType.Int32)
                    .AddParameter("@BackupFilename", backupEntry.BackupFilename, DbType.String)
                    .AddParameter("@CreatedByUserId", _currentUser.CloudUserId, DbType.Guid);

                if (await command.ExecuteNonQueryAsync() > 0)
                {
                    ((BackupEntry) backupEntry).SetInternals(false, false, DateTime.UtcNow, _currentUser.CloudUserId);

                    return true;
                }

                return false;
            }
        }
    }
}