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
        private string _deviceSerialNumber = null;
        private string _registrationNumber = null;
        private string _timeZone = null;
        private string _vehicleMake = null;
        private int _vehicleYear = 0;
        private string _extraInformation = null;

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

        public string DeviceSerialNumber
        {
            get
            {
                return _deviceSerialNumber;
            }
            set
            {
                if (_deviceSerialNumber != value)
                {
                    _deviceSerialNumber = value;

                    base.IsDirty = true;
                }
            }
        }

        public string RegistrationNumber
        {
            get
            {
                return _registrationNumber;
            }
            set
            {
                if (_registrationNumber != value)
                {
                    _registrationNumber = value;

                    base.IsDirty = true;
                }
            }
        }

        public string TimeZone
        {
            get
            {
                return _timeZone;
            }
            set
            {
                if (_timeZone != value)
                {
                    _timeZone = value;

                    base.IsDirty = true;
                }
            }
        }

        public string VehicleMake
        {
            get
            {
                return _vehicleMake;
            }
            set
            {
                if (_vehicleMake != value)
                {
                    _vehicleMake = value;

                    base.IsDirty = true;
                }
            }
        }

        public int VehicleYear
        {
            get
            {
                return _vehicleYear;
            }
            set
            {
                if (_vehicleYear != value)
                {
                    _vehicleYear = value;

                    base.IsDirty = true;
                }
            }
        }

        public string ExtraInformation
        {
            get
            {
                return _extraInformation;
            }
            set
            {
                if (_extraInformation != value)
                {
                    _extraInformation = value;

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
            string fieldname = "BackupEntryId";
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

            fieldname = "DeviceSerialNumber";
            _deviceSerialNumber = Reader.GetString(Reader.GetOrdinal(fieldname));

            fieldname = "RegistrationNumber";
            _registrationNumber = Reader.GetString(Reader.GetOrdinal(fieldname));

            fieldname = "TimeZone";
            _timeZone = Reader.GetString(Reader.GetOrdinal(fieldname));

            fieldname = "VehicleMake";
            _vehicleMake = Reader.GetString(Reader.GetOrdinal(fieldname));

            fieldname = "VehicleYear";
            _vehicleYear = Reader.GetInt32(Reader.GetOrdinal(fieldname));

            fieldname = "ExtraInformation";
            if (!Reader.IsDBNull(Reader.GetOrdinal(fieldname)))
            {
                _extraInformation = Reader.GetString(Reader.GetOrdinal(fieldname));
            }
        }
    }
}