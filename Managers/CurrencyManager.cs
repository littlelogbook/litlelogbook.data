using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities;
using LittleLogBook.Data.SqlConnectivity;

namespace LittleLogBook.Data.Managers
{
    public class CurrencyManager : ManagerBase, ICurrencyManager
    {
        private readonly IDataHandler _dataHandler;

        internal CurrencyManager(IDataHandler dataHandler, IUser currentUser)
            : base(currentUser)
        {
            _dataHandler = dataHandler;
        }

        public async Task<IEnumerable<ICurrency>> GetActiveCurrenciesAsync()
        {
            var returnValues = new List<Currency>();

            using (var command = _dataHandler.CreateCommand("GetActiveCurrencies"))
            {
                command.AddParameter("@ViewedByUserId", CurrentUser.CloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        returnValues.Add(new Currency(CurrentUser.CloudUserId, reader));
                    }
                }
            }

            return returnValues;
        }

        public async Task<IEnumerable<ICurrency>> GetCurrenciesAsync()
        {
            var returnValues = new List<Currency>();

            using (var command = _dataHandler.CreateCommand("GetCurrencies"))
            {
                command.AddParameter("@ViewedByUserId", CurrentUser.CloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        returnValues.Add(new Currency(CurrentUser.CloudUserId, reader));
                    }
                }
            }

            return returnValues;
        }

        public async Task<IEnumerable<ICurrency>> GetCurrenciesForRefreshAsync()
        {
            var returnValues = new List<Currency>();

            using (var command = _dataHandler.CreateCommand("GetCurrenciesForRefresh"))
            {
                command.AddParameter("@ViewedByUserId", CurrentUser.CloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        returnValues.Add(new Currency(CurrentUser.CloudUserId, reader));
                    }
                }
            }

            return returnValues;
        }

        public async Task<ICurrency> GetCurrencyAsync(string currencyId)
        {
            using (var command = _dataHandler.CreateCommand("GetCurrency"))
            {
                command
                    .AddParameter("@ViewedByUserId", CurrentUser.CloudUserId, DbType.Guid)
                    .AddParameter("@CurrencyId", currencyId, DbType.String);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Currency(CurrentUser.CloudUserId, reader);
                    }
                }
            }

            return null;
        }

        public async Task<bool> UpdateCurrencyAsync(ICurrency currency)
        {
            if (currency.IsDirty)
            {
                using (var command = _dataHandler.CreateCommand("UpdateCurrency"))
                {
                    command
                        .AddParameter("@CurrencyId", currency.CurrencyId, DbType.String)
                        .AddParameter("@Symbol", currency.Symbol ?? currency.CurrencyId, DbType.String)
                        .AddParameter("@CurrencyName", currency.CurrencyName ?? currency.CurrencyId, DbType.String)
                        .AddParameter("@ExchangeRate", currency.ExchangeRate, DbType.Decimal)
                        .AddParameter("@IsActive", currency.IsActive, DbType.Boolean)
                        .AddParameter("@ModifiedByUserId", CurrentUser.CloudUserId, DbType.Guid);

                    if (await command.ExecuteNonQueryAsync() > 0)
                    {
                        ((Currency) currency).SetInternals(false, false, DateTime.UtcNow, CurrentUser.CloudUserId);

                        return true;
                    }
                }
            }

            return false;
        }

        public async Task<bool> InsertCurrencyAsync(ICurrency currency)
        {
            if (currency.IsDirty)
            {
                using (var command = _dataHandler.CreateCommand("InsertCurrency"))
                {
                    command
                        .AddParameter("@CurrencyId", currency.CurrencyId, DbType.String)
                        .AddParameter("@Symbol", currency.Symbol ?? currency.CurrencyId, DbType.String)
                        .AddParameter("@CurrencyName", currency.CurrencyName ?? currency.CurrencyId, DbType.String)
                        .AddParameter("@IsActive", currency.IsActive, DbType.Boolean)
                        .AddParameter("@CreatedByUserId", CurrentUser.CloudUserId, DbType.Guid);

                    if (await command.ExecuteNonQueryAsync() > 0)
                    {
                        ((Currency) currency).SetInternals(false, false, null, null);

                        return true;
                    }
                }
            }

            return false;
        }

        public async Task<bool> DeleteCurrencyAsync(ICurrency currency)
        {
            using (var command = _dataHandler.CreateCommand("DeleteCurrency"))
            {
                command
                    .AddParameter("@CurrencyId", currency.CurrencyId, DbType.String)
                    .AddParameter("@ModifiedByUserId", CurrentUser.CloudUserId, DbType.Guid);

                if (await command.ExecuteNonQueryAsync() > 0)
                {
                    currency.IsActive = false;
                    ((Currency) currency).SetInternals(currency.IsNew, false, DateTime.UtcNow, CurrentUser.CloudUserId);

                    return true;
                }
            }

            return false;
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dataHandler?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}