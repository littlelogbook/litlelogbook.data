using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LittleLogBook.Data.Contracts
{
    public interface IBackupManager
    {
        Task<bool> FinishBackupEntryAsync(Guid backupEntryId, EnumBackupEntryStatus backupEntryStatus, long bytesTransferred, string extraInformation);
        
        Task<IBackupCost> GetBackupCostAsync(Guid cloudUserId, long dataLength);
        
        Task<IEnumerable<IBackupEntry>> GetBackupEntriesAsync(Guid cloudUserId);
        
        Task<IBackupEntry> GetBackupEntryAsync(Guid backupEntryId);
        
        Task<bool> StartBackupEntryAsync(IBackupEntry backupEntry);
        
        Task<bool> UpdateBackupEntryAsync(IBackupEntry backupEntry);
    }
}