using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Managers;
using LittleLogBook.Data.SqlConnectivity;

namespace LittleLogBook.Data.Core
{
    public class DataContext : IDataContext
    {
        private readonly IDataHandler _dataHandler;
        private readonly IUser _cloudUser;

        public DataContext(IDataHandler dataHandler, IUser cloudUser)
        {
            _dataHandler = dataHandler;
            _cloudUser = cloudUser;
        }

        public IBasketManager BasketManager => new BasketManager(_dataHandler, _cloudUser);

        public IInvoiceManager InvoiceManager => new InvoiceManager(_dataHandler, _cloudUser);

        public IUserManager UserManager => new UserManager(_dataHandler, _cloudUser);

        public IBackupManager BackupManager => new BackupManager(_dataHandler, _cloudUser);

        public IStorageProductManager StorageProductManager => new StorageProductManager(_dataHandler, _cloudUser);

        public ICountryManager CountryManager => new CountryManager(_dataHandler, _cloudUser);

        public ICurrencyManager CurrencyManager => new CurrencyManager(_dataHandler, _cloudUser);

        public IIpLocationManager IpLocationManager => new IpLocationManager(_dataHandler, _cloudUser);

        public IStatisticsManager StatisticsManager => new StatisticsManager(_dataHandler, _cloudUser);

        public IUserPurseManager UserPurseManager => new UserPurseManager(_dataHandler, _cloudUser);

        public IVerificationCodeManager VerificationCodeManager => new VerificationCodeManager(_dataHandler, _cloudUser);
    }
}