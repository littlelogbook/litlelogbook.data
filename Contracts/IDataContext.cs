namespace LittleLogBook.Data.Contracts
{
    public interface IDataContext
    {
        ICloudBackupManager BackupManager { get; }
        
        IBasketManager BasketManager { get; }
        
        ICountryManager CountryManager { get; }
        
        ICurrencyManager CurrencyManager { get; }
        
        IInvoiceManager InvoiceManager { get; }
        
        IIpLocationManager IpLocationManager { get; }
        
        IStatisticsManager StatisticsManager { get; }
        
        ICloudStorageProductManager StorageProductManager { get; }
        
        ICloudUserManager UserManager { get; }
        
        IUserPurseManager UserPurseManager { get; }
        
        IVerificationCodeManager VerificationCodeManager { get; }
    }
}