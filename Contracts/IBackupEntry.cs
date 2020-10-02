﻿using System;

namespace LittleLogBook.Data.Contracts
{
    public interface IBackupEntry
    {
        string BackupDescription { get; set; }
        
        DateTime? BackupEndDateTime { get; set; }
        
        Guid BackupEntryId { get; }
        
        EnumBackupEntryStatus BackupEntryStatus { get; }
        
        EnumBackupEntryType BackupEntryType { get; }
        
        string BackupFilename { get; set; }
        
        string BackupName { get; set; }
        
        DateTime? BackupStartDateTime { get; }

        long BytesTransferred { get; set; }
        
        string ExtraInformation { get; set; }

        Guid CloudUserId { get; }
        
        long PlannedBackupSize { get; set; }

        string DeviceSerialNumber { get; set; }

        string RegistrationNumber { get; set; }

        string TimeZone { get; set; }

        string VehicleMake { get; set; }

        int VehicleYear { get; set; }

        double BytesTransferredSizeInMegaBytes { get; }
        
        string BytesTransferredSizeText { get; }
        
        double PlannedSizeInMegaBytes { get; }
        
        string PlannedSizeText { get; }
        
        double? TotalMinutes { get; }
        
        string TotalTimeText { get; }
    }
}