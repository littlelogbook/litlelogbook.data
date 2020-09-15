using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities.Cloud;
using LittleLogBook.Data.SqlConnectivity;

namespace LittleLogBook.Data.Managers
{
    public class StorageProductManager : ManagerBase, IStorageProductManager
    {
        private readonly IDataHandler _dataHandler;

        internal StorageProductManager(IDataHandler dataHandler, IUser currentUser)
            : base(currentUser)
        {
            _dataHandler = dataHandler;
        }

        public async Task<IEnumerable<IStorageProduct>> GetActiveProductsAsync()
        {
            var returnValues = new List<CloudStorageProduct>();

            using (var command = _dataHandler.CreateCommand("GetActiveCloudStorageProducts"))
            {
                command.AddParameter("@ViewedByUserId", CurrentUser.CloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        returnValues.Add(new CloudStorageProduct(CurrentUser.CloudUserId, reader));
                    }
                }
            }

            return returnValues;
        }

        public async Task<IEnumerable<IStorageProduct>> GetProductsAsync()
        {
            var returnValues = new List<CloudStorageProduct>();

            using (var command = _dataHandler.CreateCommand("GetCloudStorageProducts"))
            {
                command.AddParameter("@ViewedByUserId", CurrentUser.CloudUserId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        returnValues.Add(new CloudStorageProduct(CurrentUser.CloudUserId, reader));
                    }
                }
            }

            return returnValues;
        }

        public async Task<IStorageProduct> GetProductAsync(Guid productId)
        {
            using (var command = _dataHandler.CreateCommand("GetCloudStorageProduct"))
            {
                command.AddParameter("@ViewedByUserId", CurrentUser.CloudUserId, DbType.Guid);
                command.AddParameter("@CloudStorageProductId", productId, DbType.Guid);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new CloudStorageProduct(CurrentUser.CloudUserId, reader);
                    }
                }
            }

            return null;
        }

        public async Task<bool> UpdateProductAsync(IStorageProduct product)
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
                command.AddParameter("@ModifiedByUserId", CurrentUser.CloudUserId, DbType.Guid);

                if (await command.ExecuteNonQueryAsync() > 0)
                {
                    ((CloudStorageProduct) product).SetInternals(false, false, DateTime.UtcNow, CurrentUser.CloudUserId);

                    return true;
                }
            }

            return false;
        }

        public async Task<bool> InsertProductAsync(IStorageProduct product)
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
                    .AddParameter("@CreatedByUserId", CurrentUser.CloudUserId, DbType.Guid);

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
                    .AddParameter("@ModifiedByUserId", CurrentUser.CloudUserId, DbType.Guid);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<bool> SuspendProductAsync(Guid productId)
        {
            using (var command = _dataHandler.CreateCommand("SuspendCloudStorageProduct"))
            {
                command
                    .AddParameter("@ProductId", productId, DbType.Guid)
                    .AddParameter("@ModifiedByUserId", CurrentUser.CloudUserId, DbType.Guid);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<bool> DeactivateProductAsync(Guid productId)
        {
            using (var command = _dataHandler.CreateCommand("DeactivateCloudStorageProduct"))
            {
                command
                    .AddParameter("@ProductId", productId, DbType.Guid)
                    .AddParameter("@ModifiedByUserId", CurrentUser.CloudUserId, DbType.Guid);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<bool> ExpireProductAsync(Guid productId)
        {
            using (var command = _dataHandler.CreateCommand("ExpireCloudStorageProduct"))
            {
                command
                    .AddParameter("@ProductId", productId, DbType.Guid)
                    .AddParameter("@ModifiedByUserId", CurrentUser.CloudUserId, DbType.Guid);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }
    }
}