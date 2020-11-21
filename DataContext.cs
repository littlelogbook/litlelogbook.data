using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Managers;
using LittleLogBook.Data.SqlConnectivity;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LittleLogBook.Data.Managers.Cloud;

namespace LittleLogBook.Data
{
    public class DataContext : IDataContext, IDisposable
    {
        public IUser CurrentUser { get; private set; }

        private IDictionary<Type, ManagerBase> _managers;
        private bool disposedValue;

        public DataContext(IDataHandler dataHandler, IUser user)
        {
            CurrentUser = user;

            _managers = new Dictionary<Type, ManagerBase>
            {
                { typeof(IBasketManager),  new BasketManager(dataHandler, user) },
                { typeof(IInvoiceManager),  new InvoiceManager(dataHandler, user) },
                { typeof(IUserManager),  new UserManager(dataHandler, user) },
                { typeof(IBackupManager),  new BackupManager(dataHandler, user) },
                { typeof(IStorageProductManager),  new StorageProductManager(dataHandler, user) },
                { typeof(ICountryManager),  new CountryManager(dataHandler, user) },
                { typeof(ICurrencyManager),  new CurrencyManager(dataHandler, user) },
                { typeof(IIpLocationManager),  new IpLocationManager(dataHandler, user) },
                { typeof(IStatisticsManager),  new StatisticsManager(dataHandler, user) },
                { typeof(IUserPurseManager),  new UserPurseManager(dataHandler, user) },
                { typeof(IVerificationCodeManager),  new VerificationCodeManager(dataHandler, user) }
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

        public static async Task<IDataContext> LogInAsync(string connectionString, string username, string password)
        {
            var dataHandler = new DataHandler(connectionString);
            var user = await Managers.Cloud.UserManager.LogInAsync(dataHandler, username, password);
            var dataContext = new DataContext(dataHandler, user);

            return dataContext;
        }
        
        public void LogOut()
        {
            if (_managers != null)
            {
                foreach (var key in _managers.Keys)
                {
                    var manager = _managers[key];

                    manager.Dispose();
                }

                _managers.Clear();

                _managers = null;
            }

            CurrentUser = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    LogOut();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            
            GC.SuppressFinalize(this);
        }
    }
}