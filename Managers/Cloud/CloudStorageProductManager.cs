using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

using LittleLogBook.Data.Common;
using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities.Cloud;

namespace LittleLogBook.Data.Managers
{
    public class CloudStorageProductManager : ICloudStorageProductManager
    {
        private readonly IDataHandler _dataHandler;
        private readonly ICloudUser _currentUser;

        public CloudStorageProductManager(IDataHandler dataHandler, ICloudUser currentUser)
        {
            _dataHandler = dataHandler;
            _currentUser = currentUser;
        }

        public async Task<IEnumerable<ICloudStorageProduct>> GetActiveProductsAsync()
        {
            var returnValues = new List<CloudStorageProduct>();

            using (var command = _dataHandler.CreateCommand("GetActiveCloudStorageProducts"))
            {
                command.AddParameter("@ViewedByUserId", _currentUser.CloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        returnValues.Add(new CloudStorageProduct(_currentUser.CloudUserId, reader));
                    }
                }
            }

            return returnValues;
        }

        public async Task<IEnumerable<ICloudStorageProduct>> GetProductsAsync()
        {
            var returnValues = new List<CloudStorageProduct>();

            using (var command = _dataHandler.CreateCommand("GetCloudStorageProducts"))
            {
                command.AddParameter("@ViewedByUserId", _currentUser.CloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        returnValues.Add(new CloudStorageProduct(_currentUser.CloudUserId, reader));
                    }
                }
            }

            return returnValues;
        }

        public async Task<ICloudStorageProduct> GetProductAsync(Guid productId)
        {
            using (var command = _dataHandler.CreateCommand("GetCloudStorageProduct"))
            {
                command.AddParameter("@ViewedByUserId", _currentUser.CloudUserId, DbType.Guid);
                command.AddParameter("@CloudStorageProductId", productId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new CloudStorageProduct(_currentUser.CloudUserId, reader);
                    }
                }
            }

            return null;
        }

        public async Task<bool> UpdateProductAsync(ICloudStorageProduct product)
        {
            if (product.IsDirty) return false;

            using (var command = _dataHandler.CreateCommand("UpdateCloudStorageProduct"))
            {
                command.AddParameter("@CloudStorageProductId", product.CloudStorageProductId, DbType.Guid);
                command.AddParameter("@ProductName", product.ProductName, DbType.String);
                command.AddParameter("@ProductImageUrl", product.ProductImageUrl, DbType.String);
                command.AddParameter("@Description", product.Description, DbType.String);
                command.AddParameter("@TotalPrice", product.TotalPrice, DbType.Decimal);
                command.AddParameter("@PriceExVat", product.PriceExVat, DbType.Decimal);
                command.AddParameter("@Credits", product.Credits, DbType.Int32);
                command.AddParameter("@ValidFrom", product.ValidFrom, DbType.DateTime);
                command.AddParameter("@ValidTo", product.ValidTo, DbType.DateTime);
                command.AddParameter("@OrderIndex", product.OrderIndex, DbType.Int32);
                command.AddParameter("@ModifiedByUserId", _currentUser.CloudUserId, DbType.Guid);

                if (await command.ExecuteNonQueryAsync() > 0)
                {
                    ((CloudStorageProduct) product).SetInternals(false, false, DateTime.UtcNow, _currentUser.CloudUserId);

                    return true;
                }
            }

            return false;
        }

        public async Task<bool> InsertProductAsync(ICloudStorageProduct product)
        {
            if (product.IsDirty) return false;

            using (var command = _dataHandler.CreateCommand("InsertCloudStorageProduct"))
            {
                command.AddParameter("@CloudStorageProductId", product.CloudStorageProductId, DbType.Guid)
                    .AddParameter("@ProductName", product.ProductName, DbType.String)
                    .AddParameter("@ProductImageUrl", product.ProductImageUrl, DbType.String)
                    .AddParameter("@Description", product.Description, DbType.String)
                    .AddParameter("@TotalPrice", product.TotalPrice, DbType.Decimal)
                    .AddParameter("@PriceExVat", product.PriceExVat, DbType.Decimal)
                    .AddParameter("@Credits", product.Credits, DbType.Int32)
                    .AddParameter("@ValidFrom", product.ValidFrom, DbType.DateTime)
                    .AddParameter("@ValidTo", product.ValidTo, DbType.DateTime)
                    .AddParameter("@OrderIndex", product.OrderIndex, DbType.Int32)
                    .AddParameter("@CreatedByUserId", _currentUser.CloudUserId, DbType.Guid);

                if (await command.ExecuteNonQueryAsync() > 0)
                {
                    ((CloudStorageProduct) product).SetInternals(false, false, null, null);

                    return true;
                }
            }

            return false;
        }

        public async Task<bool> ActivateProductAsync(Guid productId)
        {
            using (var command = _dataHandler.CreateCommand("ActivateCloudStorageProduct"))
            {
                command
                    .AddParameter("@ProductId", productId, DbType.Guid)
                    .AddParameter("@ModifiedByUserId", _currentUser.CloudUserId, DbType.Guid);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<bool> SuspendProductAsync(Guid productId)
        {
            using (var command = _dataHandler.CreateCommand("SuspendCloudStorageProduct"))
            {
                command
                    .AddParameter("@ProductId", productId, DbType.Guid)
                    .AddParameter("@ModifiedByUserId", _currentUser.CloudUserId, DbType.Guid);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<bool> DeactivateProductAsync(Guid productId)
        {
            using (var command = _dataHandler.CreateCommand("DeactivateCloudStorageProduct"))
            {
                command
                    .AddParameter("@ProductId", productId, DbType.Guid)
                    .AddParameter("@ModifiedByUserId", _currentUser.CloudUserId, DbType.Guid);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<bool> ExpireProductAsync(Guid productId)
        {
            using (var command = _dataHandler.CreateCommand("ExpireCloudStorageProduct"))
            {
                command
                    .AddParameter("@ProductId", productId, DbType.Guid)
                    .AddParameter("@ModifiedByUserId", _currentUser.CloudUserId, DbType.Guid);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }
    }
}