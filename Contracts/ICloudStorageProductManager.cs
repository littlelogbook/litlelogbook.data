using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using LittleLogBook.Data.Contracts;

namespace LittleLogBook.Data.Contracts
{
    public interface ICloudStorageProductManager
    {
        Task<bool> ActivateProductAsync(Guid productId);
        
        Task<bool> DeactivateProductAsync(Guid productId);
        
        Task<bool> ExpireProductAsync(Guid productId);
        
        Task<IEnumerable<ICloudStorageProduct>> GetActiveProductsAsync();
        
        Task<ICloudStorageProduct> GetProductAsync(Guid productId);
        
        Task<IEnumerable<ICloudStorageProduct>> GetProductsAsync();
        
        Task<bool> InsertProductAsync(ICloudStorageProduct product);
        
        Task<bool> SuspendProductAsync(Guid productId);
        
        Task<bool> UpdateProductAsync(ICloudStorageProduct product);
    }
}