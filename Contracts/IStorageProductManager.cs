using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using LittleLogBook.Data.Contracts;

namespace LittleLogBook.Data.Contracts
{
    public interface IStorageProductManager
    {
        Task<bool> ActivateProductAsync(Guid productId);
        
        Task<bool> DeactivateProductAsync(Guid productId);
        
        Task<bool> ExpireProductAsync(Guid productId);
        
        Task<IEnumerable<IStorageProduct>> GetActiveProductsAsync();
        
        Task<IStorageProduct> GetProductAsync(Guid productId);
        
        Task<IEnumerable<IStorageProduct>> GetProductsAsync();
        
        Task<bool> InsertProductAsync(IStorageProduct product);
        
        Task<bool> SuspendProductAsync(Guid productId);
        
        Task<bool> UpdateProductAsync(IStorageProduct product);
    }
}