using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using LittleLogBook.Data.Entities.Cloud;

namespace LittleLogBook.Data.Contracts
{
    public interface ICloudBackupManager
    {
        Task<bool> FinishBackupEntryAsync(Guid backupEntryId, EnumBackupEntryStatus backupEntryStatus, long bytesTransferred);
        
        Task<ICloudBackupCost> GetBackupCostAsync(Guid cloudUserId, long dataLength);
        
        Task<IEnumerable<IBackupEntry>> GetBackupEntriesAsync(Guid cloudUserId);
        
        Task<IBackupEntry> GetBackupEntryAsync(Guid backupEntryId);
        
        Task<bool> StartBackupEntryAsync(IBackupEntry backupEntry);
        
        Task<bool> UpdateBackupEntryAsync(IBackupEntry backupEntry);
    }
}