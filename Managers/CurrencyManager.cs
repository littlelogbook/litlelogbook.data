using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

using LittleLogBook.Data.Common;
using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities;

namespace LittleLogBook.Data.Managers
{
    public class CurrencyManager : ICurrencyManager
    {
        private readonly IDataHandler _dataHandler;
        private readonly ICloudUser _currentUser;

        public CurrencyManager(IDataHandler dataHandler, ICloudUser currentUser)
        {
            _dataHandler = dataHandler;
            _currentUser = currentUser;
        }

        public async Task<IEnumerable<ICurrency>> GetActiveCurrenciesAsync()
        {
            var returnValues = new List<Currency>();

            using (var command = _dataHandler.CreateCommand("GetActiveCurrencies"))
            {
                command.AddParameter("@ViewedByUserId", _currentUser.CloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        returnValues.Add(new Currency(_currentUser.CloudUserId, reader));
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
                command.AddParameter("@ViewedByUserId", _currentUser.CloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        returnValues.Add(new Currency(_currentUser.CloudUserId, reader));
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
                command.AddParameter("@ViewedByUserId", _currentUser.CloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        returnValues.Add(new Currency(_currentUser.CloudUserId, reader));
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
                    .AddParameter("@ViewedByUserId", _currentUser.CloudUserId, DbType.Guid)
                    .AddParameter("@CurrencyId", currencyId, DbType.String);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Currency(_currentUser.CloudUserId, reader);
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
                        .AddParameter("@ModifiedByUserId", _currentUser.CloudUserId, DbType.Guid);

                    if (await command.ExecuteNonQueryAsync() > 0)
                    {
                        ((Currency) currency).SetInternals(false, false, DateTime.UtcNow, _currentUser.CloudUserId);

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
                        .AddParameter("@CreatedByUserId", _currentUser.CloudUserId, DbType.Guid);

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
                    .AddParameter("@ModifiedByUserId", _currentUser.CloudUserId, DbType.Guid);

                if (await command.ExecuteNonQueryAsync() > 0)
                {
                    currency.IsActive = false;
                    ((Currency) currency).SetInternals(currency.IsNew, false, DateTime.UtcNow, _currentUser.CloudUserId);

                    return true;
                }
            }

            return false;
        }
    }
}