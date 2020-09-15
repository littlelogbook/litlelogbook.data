using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Managers;
using LittleLogBook.Data.SqlConnectivity;
using System;
using System.Collections.Generic;

namespace LittleLogBook.Data.Core
{
    public class DataContext : IDataContext
    {
        private readonly IDataHandler _dataHandler;
        private IUserManager _userManager;
        private IUser _user;

        private IDictionary<Type, ManagerBase> _managers;

        public DataContext(IDataHandler dataHandler)
            : this(dataHandler, null)
        {

        }

        public DataContext(IDataHandler dataHandler, IUser user)
        {
            _dataHandler = dataHandler;
            _user = user;

            _managers = new Dictionary<Type, ManagerBase>
            {
                { typeof(IBasketManager),  new BasketManager(_dataHandler, _user) },
                { typeof(IInvoiceManager),  new InvoiceManager(_dataHandler, _user) },
                { typeof(IUserManager),  new UserManager(_dataHandler, ChangeUser) },
                { typeof(IBackupManager),  new BackupManager(_dataHandler, _user) },
                { typeof(IStorageProductManager),  new StorageProductManager(_dataHandler, _user) },
                { typeof(ICountryManager),  new CountryManager(_dataHandler, _user) },
                { typeof(ICurrencyManager),  new CurrencyManager(_dataHandler, _user) },
                { typeof(IIpLocationManager),  new IpLocationManager(_dataHandler, _user) },
                { typeof(IStatisticsManager),  new StatisticsManager(_dataHandler, _user) },
                { typeof(IUserPurseManager),  new UserPurseManager(_dataHandler, _user) },
                { typeof(IVerificationCodeManager),  new VerificationCodeManager(_dataHandler, _user) }
            };
        }

        public IBasketManager BasketManager => (BasketManager)_managers[typeof(IBasketManager)];

        public IInvoiceManager InvoiceManager => (InvoiceManager) _managers[typeof(IInvoiceManager)];

        public IUserManager UserManager => (UserManager) _managers[typeof(IUserManager)];

        public IBackupManager BackupManager => (BackupManager) _managers[typeof(IBackupManager)];

        public IStorageProductManager StorageProductManager => (StorageProductManager) _managers[typeof(IStorageProductManager)];

        public ICountryManager CountryManager => (CountryManager) _managers[typeof(ICountryManager)];

        public ICurrencyManager CurrencyManager => (CurrencyManager) _managers[typeof(ICurrencyManager)];

        public IIpLocationManager IpLocationManager => (IpLocationManager) _managers[typeof(IIpLocationManager)];

        public IStatisticsManager StatisticsManager => (StatisticsManager) _managers[typeof(IStatisticsManager)];

        public IUserPurseManager UserPurseManager => (UserPurseManager) _managers[typeof(IUserPurseManager)];

        public IVerificationCodeManager VerificationCodeManager => (VerificationCodeManager) _managers[typeof(IVerificationCodeManager)];

        private void ChangeUser(IUser user)
        {
            foreach(var key in _managers.Keys)
            {
                _managers[key].SetUser(user);
            }
        }
    }
}