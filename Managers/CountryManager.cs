using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities;
using LittleLogBook.Data.SqlConnectivity;

namespace LittleLogBook.Data.Managers
{
    public class CountryManager : ManagerBase, ICountryManager
    {
        private readonly IDataHandler _dataHandler;

        internal CountryManager(IDataHandler dataHandler, IUser currentUser)
            : base(currentUser)
        {
            _dataHandler = dataHandler;
        }

        public async Task<ICountry> GetDefaultCountryAsync() =>
            (await GetActiveCountriesAsync())
            .FirstOrDefault(item => "ZAF".Equals(item.Iso3, StringComparison.InvariantCultureIgnoreCase));

        public async Task<IEnumerable<ICountry>> GetActiveCountriesAsync()
        {
            var returnValues = new List<Country>();

            using (var command = _dataHandler.CreateCommand("GetActiveCountries"))
            {
                command.AddParameter("@ViewedByUserId", CurrentUser.CloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        returnValues.Add(new Country(CurrentUser.CloudUserId, reader));
                    }
                }
            }

            return returnValues;
        }

        public static async Task<IEnumerable<ICountry>> GetCountriesAsync(string connectionString, Guid cloudUserId)
        {
            using (var dataHandler = new DataHandler(connectionString))
            {
                return await GetCountriesAsync(dataHandler, cloudUserId);
            }
        }

        public async Task<IEnumerable<ICountry>> GetCountriesAsync()
        {
            return await GetCountriesAsync(_dataHandler, CurrentUser.CloudUserId);
        }

        private static async Task<IEnumerable<ICountry>> GetCountriesAsync(IDataHandler dataHandler, Guid cloudUserId)
        {
            var returnValues = new List<Country>();

            using (var command = dataHandler.CreateCommand("GetCountries"))
            {
                dataHandler.AddParameter(command, "@ViewedByUserId", cloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        returnValues.Add(new Country(cloudUserId, reader));
                    }
                }
            }

            return returnValues;
        }

        public static async Task<ICountry> GetCountryByIso3Async(string connectionString, string countryIso3, Guid cloudUserId)
        {
            using (var dataHandler = new DataHandler(connectionString))
            {
                return await GetCountryByIso3Async(dataHandler, countryIso3, cloudUserId);
            }
        }
        
        public async Task<ICountry> GetCountryByIso3Async(string countryIso3)
        {
            return await GetCountryByIso3Async(_dataHandler, countryIso3, CurrentUser.CloudUserId);
        }

        private static async Task<ICountry> GetCountryByIso3Async(IDataHandler dataHandler, string countryIso3, Guid cloudUserId)
        {
            using (var command = dataHandler.CreateCommand("GetCountryByIso3"))
            {
                command
                    .AddParameter("@ViewedByUserId", cloudUserId, DbType.Guid)
                    .AddParameter("@CountryIso3", countryIso3, DbType.String);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Country(cloudUserId, reader);
                    }
                }
            }

            return null;
        }
        
        public static async Task<ICountry> GetCountryAsync(string connectionString, int countryId, Guid cloudUserId)
        {
            using (var dataHandler = new DataHandler(connectionString))
            {
                return await GetCountryAsync(dataHandler, countryId, cloudUserId);
            }
        }

        public async Task<ICountry> GetCountryAsync(int countryId)
        {
            return await GetCountryAsync(_dataHandler, countryId, CurrentUser.CloudUserId);
        }
        
        private static async Task<ICountry> GetCountryAsync(IDataHandler dataHandler, int countryId, Guid cloudUserId)
        {
            using (var command = dataHandler.CreateCommand("GetCountry"))
            {
                command
                    .AddParameter("@ViewedByUserId", cloudUserId, DbType.Guid)
                    .AddParameter("@CountryId", countryId, DbType.Int32);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Country(cloudUserId, reader);
                    }
                }
            }

            return null;
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