namespace LittleLogBook.Data.Contracts
{
    public interface IDataContext
    {
        IBackupManager BackupManager { get; }
        
        IBasketManager BasketManager { get; }
        
        ICountryManager CountryManager { get; }
        
        ICurrencyManager CurrencyManager { get; }
        
        IInvoiceManager InvoiceManager { get; }
        
        IIpLocationManager IpLocationManager { get; }
        
        IStatisticsManager StatisticsManager { get; }
        
        IStorageProductManager StorageProductManager { get; }
        
        IUserManager UserManager { get; }
        
        IUserPurseManager UserPurseManager { get; }
        
        IVerificationCodeManager VerificationCodeManager { get; }
    }
}