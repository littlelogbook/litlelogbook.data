using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LittleLogBook.Data.Contracts
{
    public interface IStatisticsManager
    {
        Task<bool> CancelBackupEntryAsync(Guid backupEntryId, long backupSize);
        
        Task<bool> FinishBackupEntryAsync(Guid backupEntryId, long backupSize);
        
        Task<IEnumerable<IStatistic>> GetStatisticsForUserAsync(Guid cloudUserId);
        
        Task<bool> InsertBackupEntryAsync(IBackupEntry backupEntry);
        
        Task<bool> UpdateBackupEntryAsync(IBackupEntry backupEntry);
    }
}