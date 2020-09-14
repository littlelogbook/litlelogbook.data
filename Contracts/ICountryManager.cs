using System.Collections.Generic;
using System.Threading.Tasks;

namespace LittleLogBook.Data.Contracts
{
    public interface ICountryManager
    {
        Task<IEnumerable<ICountry>> GetActiveCountriesAsync();
        
        Task<IEnumerable<ICountry>> GetCountriesAsync();
        
        Task<ICountry> GetCountryAsync(int countryId);
        
        Task<ICountry> GetCountryByIso3Async(string countryIso3);
        
        Task<ICountry> GetDefaultCountryAsync();
    }
}