using System.Collections.Generic;
using System.Threading.Tasks;

namespace LittleLogBook.Data.Contracts
{
    public interface ICurrencyManager
    {
        Task<bool> DeleteCurrencyAsync(ICurrency currency);
        
        Task<IEnumerable<ICurrency>> GetActiveCurrenciesAsync();
        
        Task<IEnumerable<ICurrency>> GetCurrenciesAsync();
        
        Task<IEnumerable<ICurrency>> GetCurrenciesForRefreshAsync();
        
        Task<ICurrency> GetCurrencyAsync(string currencyId);
        
        Task<bool> InsertCurrencyAsync(ICurrency currency);
        
        Task<bool> UpdateCurrencyAsync(ICurrency currency);
    }
}