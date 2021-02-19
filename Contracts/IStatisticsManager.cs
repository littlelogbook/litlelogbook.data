using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LittleLogBook.Data.Contracts
{
    public interface IStatisticsManager
    {
        Task<IEnumerable<INameValueValueStatistic>> GetCloudBackupRestoreDataTrafficPerCountry();

        Task<IEnumerable<INameValueValueStatistic>> GetCloudBackupsRestoresPerCountry();

        Task<IEnumerable<INameValueValueStatistic>> GetCloudCreditsPerCountry();

        Task<IEnumerable<INameValueValueStatistic>> GetCloudSpendPerCountry();

        Task<IEnumerable<INameValueStatistic>> GetInstallationsPerCountry();

        Task<IEnumerable<INameValueStatistic>> GetDevicesPerCountry();

        Task<IEnumerable<INameValueValueStatistic>> GetCloudUsersPerCountry();

        Task<IEnumerable<INameValueStatistic>> GetActiveDevicesPerCountry();

        Task<bool> CancelBackupEntryAsync(Guid backupEntryId, long backupSize);
        
        Task<bool> FinishBackupEntryAsync(Guid backupEntryId, long backupSize);

        Task<IEnumerable<INameValueValueStatistic>> GetSqlConnections();
        
        Task<IEnumerable<IStatistic>> GetStatisticsForUserAsync(Guid cloudUserId);
        
        Task<bool> InsertBackupEntryAsync(IBackupEntry backupEntry);
        
        Task<bool> UpdateBackupEntryAsync(IBackupEntry backupEntry);
    }
}