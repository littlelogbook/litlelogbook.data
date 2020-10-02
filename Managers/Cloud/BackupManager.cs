using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities.Cloud;
using LittleLogBook.Data.SqlConnectivity;

namespace LittleLogBook.Data.Managers
{
    public class BackupManager : ManagerBase, IBackupManager
    {
        private readonly IDataHandler _dataHandler;

        internal BackupManager(IDataHandler dataHandler, IUser currentUser)
            : base(currentUser)
        {
            _dataHandler = dataHandler;
        }

        public async Task<IBackupCost> GetBackupCostAsync(Guid cloudUserId, long dataLength)
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
                command.AddParameter("@ViewedByUserId", CurrentUser.CloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new BackupEntry(CurrentUser.CloudUserId, reader);
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
                command.AddParameter("@ViewedByUserId", CurrentUser.CloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        returnValues.Add(new BackupEntry(CurrentUser.CloudUserId, reader));
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
                command.AddParameter("@ModifiedByUserId", CurrentUser.CloudUserId, DbType.Guid);

                if (await command.ExecuteNonQueryAsync() > 0)
                {
                    ((BackupEntry)backupEntry).SetInternals(false, false, DateTime.UtcNow, CurrentUser.CloudUserId);

                    return true;
                }
            }

            return false;
        }

        public async Task<bool> FinishBackupEntryAsync(Guid backupEntryId, EnumBackupEntryStatus backupEntryStatus, long bytesTransferred, string extraInformation)
        {
            using (var command = _dataHandler.CreateCommand("FinishBackupEntry"))
            {
                command.AddParameter("@BackupEntryId", backupEntryId, DbType.Guid);
                command.AddParameter("@BytesTransferred", bytesTransferred, DbType.Int64);
                command.AddParameter("@BackupEntryStatusId", backupEntryStatus, DbType.Int64);
                command.AddParameter("@ModifiedByUserId", CurrentUser.CloudUserId, DbType.Guid);
                command.AddParameter("@ExtraInformation", extraInformation, DbType.String);

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
                command.AddParameter("@DeviceSerialNumber", backupEntry.DeviceSerialNumber, DbType.String);
                command.AddParameter("@RegistrationNumber", backupEntry.RegistrationNumber, DbType.String);
                command.AddParameter("@TimeZone", backupEntry.TimeZone, DbType.String);
                command.AddParameter("@VehicleMake", backupEntry.VehicleMake, DbType.String);
                command.AddParameter("@VehicleYear", backupEntry.VehicleYear, DbType.Int32);
                command.AddParameter("@CreatedByUserId", CurrentUser.CloudUserId, DbType.Guid);

                if (await command.ExecuteNonQueryAsync() > 0)
                {
                    ((BackupEntry)backupEntry).SetInternals(false, false, DateTime.UtcNow, CurrentUser.CloudUserId);

                    return true;
                }
            }

            return false;
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
