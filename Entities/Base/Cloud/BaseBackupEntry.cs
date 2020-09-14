using System;
using System.Data;
using System.Data.SqlClient;

namespace LittleLogBook.Data.Entities.Base.Cloud
{
    public class BaseBackupEntry : BaseEntity
    {
        private Guid _backupEntryId = Guid.NewGuid();
        private Guid _cloudUserId = Guid.Empty;
        private EnumBackupEntryType _backupEntryType = EnumBackupEntryType.Unknown;
        private EnumBackupEntryStatus _backupEntryStatus = EnumBackupEntryStatus.Unknown;
        private string _backupName = null;
        private string _backupFilename = null;
        private string _backupDescription = null;
        private DateTime _backupStartDateTime = DateTime.MinValue;
        private DateTime? _backupEndDateTime = null;
        private long _plannedBackupSize = 0;
        private long _bytesTransferred = 0;

        public Guid BackupEntryId
        {
            get
            {
                return _backupEntryId;
            }
        }

        public Guid CloudUserId
        {
            get
            {
                return _cloudUserId;
            }
        }

        public EnumBackupEntryType BackupEntryType
        {
            get
            {
                return _backupEntryType;
            }
        }

        public EnumBackupEntryStatus BackupEntryStatus
        {
            get
            {
                return _backupEntryStatus;
            }
        }

        public DateTime? BackupStartDateTime
        {
            get
            {
                return _backupStartDateTime;
            }
        }

        public DateTime? BackupEndDateTime
        {
            get
            {
                return _backupEndDateTime;
            }
            set
            {
                if (_backupEndDateTime != value)
                {
                    _backupEndDateTime = value;

                    base.IsDirty = true;
                }
            }
        }

        public string BackupName
        {
            get
            {
                return _backupName;
            }
            set
            {
                if (_backupName != value)
                {
                    _backupName = value;

                    base.IsDirty = true;
                }
            }
        }

        public string BackupFilename
        {
            get
            {
                return _backupFilename;
            }
            set
            {
                if (_backupFilename != value)
                {
                    _backupFilename = value;

                    base.IsDirty = true;
                }
            }
        }

        public string BackupDescription
        {
            get
            {
                return _backupDescription;
            }
            set
            {
                if (_backupDescription != value)
                {
                    _backupDescription = value;

                    base.IsDirty = true;
                }
            }
        }

        public long PlannedBackupSize
        {
            get
            {
                return _plannedBackupSize;
            }
            set
            {
                if (_plannedBackupSize != value)
                {
                    _plannedBackupSize = value;

                    base.IsDirty = true;
                }
            }
        }

        public long BytesTransferred
        {
            get
            {
                return _bytesTransferred;
            }
            set
            {
                if (_bytesTransferred != value)
                {
                    _bytesTransferred = value;

                    base.IsDirty = true;
                }
            }
        }

        public BaseBackupEntry(Guid CreatedByUserId, Guid CloudUserId, EnumBackupEntryType BackupEntryType, string BackupFilename) : base(CreatedByUserId)
        {
            _cloudUserId = CloudUserId;
            _backupEntryType = BackupEntryType;
            _backupEntryStatus = EnumBackupEntryStatus.Current;
            _backupFilename = BackupFilename;
        }

        public BaseBackupEntry(Guid CreatedByUserId, Guid BackupEntryId, Guid CloudUserId, EnumBackupEntryType BackupEntryType, string BackupFilename) : base(CreatedByUserId)
        {
            _backupEntryId = BackupEntryId;
            _cloudUserId = CloudUserId;
            _backupEntryType = BackupEntryType;
            _backupFilename = BackupFilename;
            _backupEntryStatus = EnumBackupEntryStatus.Current;
        }

        public BaseBackupEntry(Guid ViewedByUserId, SqlDataReader Reader) : base(ViewedByUserId, Reader)
        {
            string fieldname = null;

            fieldname = "BackupEntryId";
            _backupEntryId = Reader.GetGuid(Reader.GetOrdinal(fieldname));

            fieldname = "CloudUserId";
            _cloudUserId = Reader.GetGuid(Reader.GetOrdinal(fieldname));

            fieldname = "BackupEntryTypeId";
            _backupEntryType = (EnumBackupEntryType) Reader.GetInt32(Reader.GetOrdinal(fieldname));

            fieldname = "BackupEntryStatusId";
            _backupEntryStatus = (EnumBackupEntryStatus) Reader.GetInt32(Reader.GetOrdinal(fieldname));

            fieldname = "BackupStartDateTime";
            _backupStartDateTime = Reader.GetDateTime(Reader.GetOrdinal(fieldname));

            fieldname = "BackupEndDateTime";
            if (!Reader.IsDBNull(Reader.GetOrdinal(fieldname)))
            {
                _backupEndDateTime = Reader.GetDateTime(Reader.GetOrdinal(fieldname));
            }

            fieldname = "BackupFilename";
            _backupFilename = Reader.GetString(Reader.GetOrdinal(fieldname));

            fieldname = "BackupName";
            if (!Reader.IsDBNull(Reader.GetOrdinal(fieldname)))
            {
                _backupName = Reader.GetString(Reader.GetOrdinal(fieldname));
            }

            fieldname = "BackupDescription";
            if (!Reader.IsDBNull(Reader.GetOrdinal(fieldname)))
            {
                _backupDescription = Reader.GetString(Reader.GetOrdinal(fieldname));
            }

            fieldname = "PlannedBackupSize";
            _plannedBackupSize = Reader.GetInt64(Reader.GetOrdinal(fieldname));

            fieldname = "BytesTransferred";
            if (!Reader.IsDBNull(Reader.GetOrdinal(fieldname)))
            {
                _bytesTransferred = Reader.GetInt64(Reader.GetOrdinal(fieldname));
            }
        }
    }
}