using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using LittleLogBook.Data.Common;
using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities;

namespace LittleLogBook.Data.Managers
{
    public class CountryManager : ICountryManager
    {
        private readonly IDataHandler _dataHandler;
        private readonly ICloudUser _currentUser;

        public CountryManager(IDataHandler dataHandler, ICloudUser currentUser)
        {
            _dataHandler = dataHandler;
            _currentUser = currentUser;
        }

        public async Task<ICountry> GetDefaultCountryAsync() =>
            (await GetActiveCountriesAsync())
            .FirstOrDefault(item => "ZAF".Equals(item.Iso3, StringComparison.InvariantCultureIgnoreCase));

        public async Task<IEnumerable<ICountry>> GetActiveCountriesAsync()
        {
            var returnValues = new List<Country>();

            using (var command = _dataHandler.CreateCommand("GetActiveCountries"))
            {
                command.AddParameter("@ViewedByUserId", _currentUser.CloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        returnValues.Add(new Country(_currentUser.CloudUserId, reader));
                    }
                }
            }

            return returnValues;
        }

        public async Task<IEnumerable<ICountry>> GetCountriesAsync()
        {
            var returnValues = new List<Country>();

            using (var command = _dataHandler.CreateCommand("GetCountries"))
            {
                _dataHandler.AddParameter(command, "@ViewedByUserId", _currentUser.CloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        returnValues.Add(new Country(_currentUser.CloudUserId, reader));
                    }
                }
            }

            return returnValues;
        }

        public async Task<ICountry> GetCountryByIso3Async(string countryIso3)
        {
            using (var command = _dataHandler.CreateCommand("GetCountryByIso3"))
            {
                command
                    .AddParameter("@ViewedByUserId", _currentUser.CloudUserId, DbType.Guid)
                    .AddParameter("@CountryIso3", countryIso3, DbType.String);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Country(_currentUser.CloudUserId, reader);
                    }
                }
            }

            return null;
        }

        public async Task<ICountry> GetCountryAsync(int countryId)
        {
            using (var command = _dataHandler.CreateCommand("GetCountry"))
            {
                command
                    .AddParameter("@ViewedByUserId", _currentUser.CloudUserId, DbType.Guid)
                    .AddParameter("@CountryId", countryId, DbType.Int32);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Country(Constants.SystemUserId, reader);
                    }
                }
            }

            return null;
        }
    }
}