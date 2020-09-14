using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

using LittleLogBook.Data.Common;
using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities.Cloud;

namespace LittleLogBook.Data.Managers
{
    public class CloudBackupManager : ICloudBackupManager
    {
        private readonly IDataHandler _dataHandler;
        private readonly ICloudUser _currentUser;

        public CloudBackupManager(IDataHandler dataHandler, ICloudUser currentUser)
        {
            _dataHandler = dataHandler;
            _currentUser = currentUser;
        }

        public async Task<ICloudBackupCost> GetBackupCostAsync(Guid cloudUserId, long dataLength)
        {
            if (Guid.Empty.Equals(cloudUserId))
            {
                throw new ArgumentNullException(nameof(cloudUserId), "The specified user ID may not be empty");
            }

            using (var command = _dataHandler.CreateCommand("GetCloudBackupCost"))
            {
                command.AddParameter("@CloudUserId", cloudUserId, DbType.Guid);
                command.AddParameter("@DataLength", dataLength, DbType.Int64);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new CloudBackupCost(reader);
                    }
                }
            }

            return null;
        }

        public async Task<IBackupEntry> GetBackupEntryAsync(Guid backupEntryId)
        {
            if (Guid.Empty.Equals(backupEntryId))
            {
                throw new ArgumentNullException(nameof(backupEntryId), "The specified backup entry ID may not be empty");
            }

            using (var command = _dataHandler.CreateCommand("GetBackupEntry"))
            {
                command.AddParameter("@BackupEntryId", backupEntryId, DbType.Guid);
                command.AddParameter("@ViewedByUserId", _currentUser.CloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new BackupEntry(_currentUser.CloudUserId, reader);
                    }
                }
            }

            return null;
        }

        public async Task<IEnumerable<IBackupEntry>> GetBackupEntriesAsync(Guid cloudUserId)
        {
            var returnValues = new List<BackupEntry>();

            if (Guid.Empty.Equals(cloudUserId))
            {
                throw new ArgumentNullException("The specified cloud user ID may not be empty");
            }

            using (var command = _dataHandler.CreateCommand("GetBackupEntries"))
            {
                command.AddParameter("@CloudUserId", cloudUserId, DbType.Guid);
                command.AddParameter("@ViewedByUserId", _currentUser.CloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        returnValues.Add(new BackupEntry(_currentUser.CloudUserId, reader));
                    }
                }
            }

            return returnValues;
        }

        public async Task<bool> UpdateBackupEntryAsync(IBackupEntry backupEntry)
        {
            using (var command = _dataHandler.CreateCommand("UpdateBackupEntry"))
            {
                command.AddParameter("@BackupEntryId", backupEntry.BackupEntryId, DbType.Guid);
                command.AddParameter("@BytesTransferred", backupEntry.BytesTransferred, DbType.Int64);
                command.AddParameter("@BackupName", backupEntry.BackupName, DbType.String);
                command.AddParameter("@BackupDescription", backupEntry.BackupDescription, DbType.String);
                command.AddParameter("@ModifiedByUserId", _currentUser.CloudUserId, DbType.Guid);

                if (await command.ExecuteNonQueryAsync() > 0)
                {
                    ((BackupEntry)backupEntry).SetInternals(false, false, DateTime.UtcNow, _currentUser.CloudUserId);

                    return true;
                }
            }

            return false;
        }

        public async Task<bool> FinishBackupEntryAsync(Guid backupEntryId, EnumBackupEntryStatus backupEntryStatus, long bytesTransferred)
        {
            using (var command = _dataHandler.CreateCommand("FinishBackupEntry"))
            {
                command.AddParameter("@BackupEntryId", backupEntryId, DbType.Guid);
                command.AddParameter("@BytesTransferred", bytesTransferred, DbType.Int64);
                command.AddParameter("@BackupEntryStatusId", backupEntryStatus, DbType.Int64);
                command.AddParameter("@ModifiedByUserId", _currentUser.CloudUserId, DbType.Guid);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<bool> StartBackupEntryAsync(IBackupEntry backupEntry)
        {
            using (var command = _dataHandler.CreateCommand("StartBackupEntry"))
            {
                command.AddParameter("@BackupEntryId", backupEntry.BackupEntryId, DbType.Guid);
                command.AddParameter("@CloudUserId", backupEntry.CloudUserId, DbType.Guid);
                command.AddParameter("@BackupEntryTypeId", (int) backupEntry.BackupEntryType, DbType.Int32);
                command.AddParameter("@BackupFilename", backupEntry.BackupFilename, DbType.String);
                command.AddParameter("@PlannedBackupSize", backupEntry.PlannedBackupSize, DbType.Int64);
                command.AddParameter("@CreatedByUserId", _currentUser.CloudUserId, DbType.Guid);

                if (await command.ExecuteNonQueryAsync() > 0)
                {
                    ((BackupEntry)backupEntry).SetInternals(false, false, DateTime.UtcNow, _currentUser.CloudUserId);

                    return true;
                }
            }

            return false;
        }
    }
}
