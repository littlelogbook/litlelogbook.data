namespace LittleLogBook.Data.Contracts
{
    public interface ICountry
    {
        int CountryCode { get; }
        
        int CountryId { get; }
        
        bool IsActive { get; }
        
        string Iso2 { get; }
        
        string Iso3 { get; }
        
        string Name { get; }
        
        double TaxRate { get; }
    }
}