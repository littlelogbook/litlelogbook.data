namespace LittleLogBook.Data.Contracts
{
    public interface ICurrency
    {
        string CurrencyId { get; }
        
        string CurrencyName { get; set; }
        
        double ExchangeRate { get; set; }
        
        bool IsActive { get; set; }
        
        string Symbol { get; set; }

        public string ExchangeRateText { get; }

        bool IsDirty { get; }

        bool IsNew { get; }
    }
}