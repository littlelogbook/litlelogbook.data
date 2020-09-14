using System;
using System.Data;
using System.Data.SqlClient;
using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities.Base.Cloud;

namespace LittleLogBook.Data.Entities.Cloud
{
    public class BackupEntry : BaseBackupEntry, IBackupEntry
    {
        public BackupEntry(Guid CreatedByUserId, Guid CloudUserId, EnumBackupEntryType BackupEntryType, string BackupFilename)
            : base(CreatedByUserId, CloudUserId, BackupEntryType, BackupFilename)
        {

        }

        public BackupEntry(Guid ViewedByUserId, SqlDataReader Reader) : base(ViewedByUserId, Reader)
        {

        }

        public BackupEntry(Guid CreatedByUserId, Guid BackupEntryId, Guid CloudUserId, EnumBackupEntryType BackupEntryType, string BackupFilename)
            : base(CreatedByUserId, BackupEntryId, CloudUserId, BackupEntryType, BackupFilename)
        {

        }

        public double PlannedSizeInMegaBytes
        {
            get
            {
                return Math.Round((double) base.PlannedBackupSize / 1000000, 2);
            }
        }

        public double BytesTransferredSizeInMegaBytes
        {
            get
            {
                return Math.Round((double) base.BytesTransferred / 1000000, 2);
            }
        }

        public string PlannedSizeText
        {
            get
            {
                string returnValue = null;

                if (base.PlannedBackupSize < 1000000)
                {
                    returnValue = string.Format("{0:F2} KB", Math.Round((double) base.PlannedBackupSize / 1000, 2));
                }
                else if (base.PlannedBackupSize < 1000000000)
                {
                    returnValue = string.Format("{0:F2} MB", Math.Round((double) base.PlannedBackupSize / 1000000, 2));
                }
                else if (base.PlannedBackupSize < 1000000000000)
                {
                    returnValue = string.Format("{0:F2} GB", Math.Round((double) base.PlannedBackupSize / 1000000000, 2));
                }

                return returnValue;
            }
        }

        public string BytesTransferredSizeText
        {
            get
            {
                string returnValue = null;

                if (base.BytesTransferred < 1000000)
                {
                    returnValue = string.Format("{0:F2} KB", Math.Round((double) base.BytesTransferred / 1000, 2));
                }
                else if (base.BytesTransferred < 1000000000)
                {
                    returnValue = string.Format("{0:F2} MB", Math.Round((double) base.BytesTransferred / 1000000, 2));
                }
                else if (base.BytesTransferred < 1000000000000)
                {
                    returnValue = string.Format("{0:F2} GB", Math.Round((double) base.BytesTransferred / 1000000000, 2));
                }

                return returnValue;
            }
        }

        private double? _totalMinutes = null;

        public double? TotalMinutes
        {
            get
            {
                if (!_totalMinutes.HasValue && BackupEndDateTime.HasValue)
                {
                    _totalMinutes = Math.Round((BackupEndDateTime.Value - BackupStartDateTime.Value).TotalMinutes, 2);
                }

                return _totalMinutes;
            }
        }

        private string _totalTimeText = null;

        public string TotalTimeText
        {
            get
            {
                if (_totalTimeText == null)
                {
                    if (TotalMinutes.HasValue)
                    {
                        int totalHours = 0;
                        int totalMinutes = 0;
                        int totalSeconds = 0;

                        double totalRemainder = TotalMinutes.Value * 60;

                        totalHours = (int) (totalRemainder / 60);
                        totalRemainder = totalRemainder % 60;

                        totalMinutes = (int) (totalRemainder / 60);
                        totalRemainder = totalRemainder % 60;

                        totalSeconds = (int) (totalRemainder);

                        _totalTimeText = totalHours.ToString("D2") + ":" + totalMinutes.ToString("D2") + ":" + totalSeconds.ToString("D2");
                    }
                    else
                    {
                        _totalTimeText = "";
                    }
                }

                return _totalTimeText;
            }
        }
    }
}